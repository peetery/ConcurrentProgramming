using Data;
using System;
using System.Collections.ObjectModel;
using System.Numerics;

namespace Logic
{
    public class LogicAPI
    {
        private readonly DataAPI _dataAPI;
        private CancellationToken _cancellationToken;
        private List<Task> _tasks = new List<Task>();

        public Table table { get; }
        public ObservableCollection<BallLogic> balls { get; }

        public LogicAPI()
        {
           balls = new ObservableCollection<BallLogic>();
           _dataAPI = DataAPI.createDataAPI();
           BallLogic.SetTable(table);
        }

        public void RunSimulation()
        {
            _cancellationToken = CancellationToken.None;

            foreach (BallLogic ball in balls)
            {
                Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        try
                        {
                            _cancellationToken.ThrowIfCancellationRequested();
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                        ball.setPosition();
                    }
                });
                _tasks.Add(task);
            }
        }

        public void StopSimulation()
        {
            _cancellationToken = new CancellationToken(true);

            foreach (Task task in _tasks)
            {
                task.Wait();
            }

            _tasks.Clear();
            balls.Clear();
        }
        
        public BallLogic createBall(int radius, Vector2 position)
        {
            Vector2 basicVelocity = new Vector2((float)0.005, (float)0.005);
            Ball ball = _dataAPI.getBallData(radius, position, basicVelocity);
            BallLogic ballLogic = new BallLogic(ball);
            return ballLogic;
        }

        public void createBalls(int amount, int radius)
        {
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                Vector2 position = new Vector2((float)random.Next(0, table.Width - radius), (float)random.Next(0, table.Height - radius));
                BallLogic newBall = createBall(radius, position);
                balls.Add(newBall);
            }
        }

        public void deleteBalls()
        {
            balls.Clear();
        }
    }
}
