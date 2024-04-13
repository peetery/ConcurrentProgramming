using Logic;
using Data;
using System.Collections.ObjectModel;
using System.Numerics;

namespace LogicTest
{
    [TestFixture]
    public class BallLogicTests
    {
        private LogicAbstractAPI _logicAPI;

        [SetUp]
        public void Initialize()
        {
            _logicAPI = LogicAbstractAPI.CreateAPI();
        }

        [Test]
        public void logicAPIConstructorTest()
        {
            int width = 500;
            int height = 500;
            Assert.AreEqual(_logicAPI.balls.Count, 0);
            Assert.AreEqual(width, _logicAPI.table.Width);
            Assert.AreEqual(height, _logicAPI.table.Height);
        }

        [Test]
        public void ballConstructorTest()
        {
            Vector2 testPosition = new Vector2(2, 4);
            Vector2 testVelocity = new Vector2(3, 6);
            int radius = 10;
            Ball ball = new Ball(radius, testPosition, testVelocity);
            Assert.AreEqual(radius, ball.Radius);
            Assert.AreEqual(testPosition, ball.Position);
            Assert.AreEqual(testVelocity, ball.Velocity);
        }

        [Test]
        public void changeVelocityTest()
        {
            Vector2 testPosition = new Vector2(2, 4);
            Vector2 testVelocity = new Vector2(3, 6);
            int radius = 10;
            Ball ball = new Ball(radius, testPosition, testVelocity);
            ball.Velocity = new Vector2(6, 12);
            Assert.AreEqual(6, ball.Velocity.X);
            Assert.AreEqual(12, ball.Velocity.Y);
        }

        [Test]
        public void setPositionTest()
        {
            Vector2 testPosition = new Vector2(_logicAPI.table.Width, _logicAPI.table.Height);
            Vector2 testVelocity = new Vector2(1, 2);
            int radius = 10;
            Ball ball = new Ball(radius, testPosition, testVelocity);
            BallLogic ballLogic = new BallLogic(ball);
            ballLogic.setPosition();
            Assert.AreNotEqual(_logicAPI.table.Width, ball.Position.X);
            Assert.AreNotEqual(_logicAPI.table.Height, ball.Position.Y);
        }

        [Test]
        public void createDeleteBallsTest()
        {
            int amount = 10;
            int radius = 25;
            _logicAPI.createBalls(amount, radius);
            Assert.AreEqual(amount, _logicAPI.balls.Count);

            foreach (BallLogic ball in _logicAPI.balls)
            {
                Assert.IsTrue(ball.X > 0);
                Assert.IsTrue(ball.Y > 0);
                Assert.IsTrue(ball.X <= _logicAPI.table.Width - ball.Radius);
                Assert.IsTrue(ball.Y <= _logicAPI.table.Height - ball.Radius);
            }

            _logicAPI.deleteBalls();
            Assert.AreEqual(0, _logicAPI.balls.Count);
        }
    }
}
