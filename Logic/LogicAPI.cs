﻿using Data;
using System;
using System.Collections.ObjectModel;
using System.Numerics;
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

        public LogicAPI()
        {
           _dataAPI = DataAPI.createDataAPI();
            table = _dataAPI.getTableData(500, 500);
           BallLogic.SetTable(table);
        }

        public override void RunSimulation()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Barrier barrier = new Barrier(balls.Count);
            float timeTravel = 0.01f;

            foreach (BallLogic ball in balls)
            {
                Task task = Task.Run(async () =>
                {
                    barrier.SignalAndWait();
                    while (true)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(timeTravel), _cancellationTokenSource.Token);
                        if (_cancellationTokenSource.Token.IsCancellationRequested)
                            break;
                        foreach (BallLogic otherBall in balls)
                        {
                            lock (balls)
                            {
                                if (ball == otherBall)
                                {
                                    continue;
                                }
                                else if (ball.ballCollision(otherBall))
                                {
                                    ball.handleBallColission(otherBall);
                                }

                            }
                        }

                        ball.setPosition();
                    }
                }, _cancellationTokenSource.Token);
                _tasks.Add(task);
            }
        }

        public override void StopSimulation()
        {
            _cancellationTokenSource?.Cancel();

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
                Vector2 position = new Vector2((float)random.Next(0, table.Width - radius), (float)random.Next(0, table.Height - radius));
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
