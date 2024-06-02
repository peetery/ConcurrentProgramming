using Data;
using System;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Text.Json;
using System.Threading;

namespace Logic
{
    public class LogicAPI : LogicAbstractAPI
    {
        private readonly DataAPI _dataAPI;
        private CancellationTokenSource _cancellationTokenSource;
        private List<Task> _tasks = new List<Task>();

        public override Table table { get; }
        public override ObservableCollection<BallLogic> balls { get; } = new ObservableCollection<BallLogic>();

        public LogicAPI() : this(DataAPI.createDataAPI()) { }

        public LogicAPI(DataAPI dataAPI)
        {
            _dataAPI = dataAPI;
            table = _dataAPI.getTableData(500, 500);
            BallLogic.SetTable(table);
        }

        public override void RunSimulation()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancellationTokenSource.Token;
            Barrier barrier = new Barrier(balls.Count);

            foreach (BallLogic ball in balls)
            {
                Task task = Task.Run(async () =>
                {
                    barrier.SignalAndWait();
                    var timer = new System.Timers.Timer();
                    timer.Elapsed += async (_, elapsedArgs) =>
                    {
                        if (token.IsCancellationRequested)
                        {
                            timer.Stop();
                            timer.Dispose();
                            return;
                        }

                        List<string> logsToSave = new List<string>();

                        lock (balls)
                        {
                            ball.setPosition();
                            foreach (BallLogic otherBall in balls)
                            {
                                if (ball == otherBall)
                                {
                                    continue;
                                }
                                else if (ball.ballCollision(otherBall))
                                {
                                    ball.handleBallColission(otherBall);
                                    // Logowanie kolizji
                                    var logData = new
                                    {
                                        Event = "Collision",
                                        Ball1 = ball,
                                        Ball2 = otherBall,
                                        Timestamp = DateTime.UtcNow
                                    };
                                    string jsonData = JsonSerializer.Serialize(logData);
                                    logsToSave.Add(jsonData);
                                }
                            }
                        }

                        foreach (var log in logsToSave)
                        {
                            await _dataAPI.LogDiagnosticDataAsync(log);
                        }
                    };
                    timer.Interval = 8.8888; // 1/113Hz
                    timer.Enabled = true;

                    WaitHandle.WaitAny(new[] { token.WaitHandle });
                });

                _tasks.Add(task);
            }
        }

        public override void StopSimulation()
        {
            _cancellationTokenSource.Cancel();

            try
            {
                Task.WhenAll(_tasks).Wait();
            }
            catch (AggregateException) { }

            _tasks.Clear();
            balls.Clear();
        }

        public override BallLogic createBall(int radius, Vector2 position)
        {
            Vector2 basicVelocity = new Vector2((float)0.01, (float)0.01);
            Ball ball = _dataAPI.getBallData(radius, position, basicVelocity, radius/2);
            BallLogic ballLogic = new BallLogic(ball);
            return ballLogic;
        }

        public override void createBalls(int amount, int radius)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                Vector2 position;
                bool collides;
                do
                {
                    position = new Vector2((float)random.Next(0, table.Width - radius), (float)random.Next(0, table.Height - radius));
                    collides = false;
                    foreach (var existingBall in balls)
                    {
                        float distance = Vector2.Distance(position, existingBall.Position);
                        if (distance < radius + existingBall.Radius)
                        {
                            collides = true;
                            break;
                        }
                    }

                } while (collides);
                BallLogic newBall = createBall(radius, position);
                balls.Add(newBall);
            }
        }

        public override void deleteBalls()
        {
            balls.Clear();
        }
    }
}
