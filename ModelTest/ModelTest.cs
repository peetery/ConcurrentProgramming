using Data;
using Logic;
using Model;
using System.Collections.ObjectModel;
using System.Numerics;

namespace ModelTest
{
    public class ModelTests
    {
        private class LogicMockClass : LogicAbstractAPI
        {
            public bool StartSimulationCall { get; set; }
            public bool StopSimulationCall { get; set; }
            public override ObservableCollection<BallLogic> balls { get; } = new ObservableCollection<BallLogic>();
            public override Table table => new Table(500, 500);
            public override BallLogic createBall(int radius, Vector2 position)
            {
                throw new NotImplementedException();
            }
            public override void createBalls(int amount, int radius)
            {
                for (int i = 0; i < amount; i++)
                {
                    balls.Add(new BallLogic(new Ball(radius, Vector2.Zero, Vector2.Zero)));
                }
            }

            public override void deleteBalls()
            {
                throw new NotImplementedException();
            }

            public override void RunSimulation()
            {
                StartSimulationCall = true;
            }

            public override void StopSimulation()
            {
                StopSimulationCall = true;
            }
        }

        private ModelAbstractAPI _modelLayer;
        private LogicAbstractAPI _logicLayer;

        [SetUp]
        public void Initialize()
        {
            _logicLayer = new LogicMockClass();
            _modelLayer = ModelAbstractAPI.CreateModelAPI(_logicLayer);
        }

        [Test]
        public void getWidthTest()
        {
            int expected_value = 500;
            int actual_value = _modelLayer.Width;
            Assert.AreEqual(expected_value, actual_value);
        }

        [Test]
        public void getHeightTest()
        {
            int expected_value = 500;
            int actual_value = _modelLayer.Height;
            Assert.AreEqual(expected_value, actual_value);
        }

        [Test]
        public void createBallsTest()
        {
            int radius = 25;
            int amount = 10;
            ObservableCollection<BallLogic> balls = _modelLayer.createBalls(amount, radius);
            Assert.IsNotNull(balls);
            Assert.AreEqual(amount, balls.Count);
        }
    }
}