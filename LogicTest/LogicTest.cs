using Data;
using Logic;
using System.Numerics;

namespace LogicTest
{
    public class Tests
    {
        private BallLogic _ballLogic;
        private Table _table;

        [SetUp]
        public void Setup()
        {
            Ball ball = new Ball(10, new Vector2(50, 50), new Vector2(1, 1));
            _ballLogic = new BallLogic(ball);
            _table = new Table(100, 100);
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.AreEqual(50, _ballLogic.X);
            Assert.AreEqual(50, _ballLogic.Y);
            Assert.AreEqual(10, _ballLogic.Radius);
            Assert.AreEqual(new Vector2(1, 1), _ballLogic.Velocity);
        }

        [Test]
        public void setVelocityTest()
        {
            _ballLogic.Velocity = new Vector2(2, 2);
            Assert.AreEqual(new Vector2(2, 2), _ballLogic.Velocity);
        }

        [Test]
        public void setPositionTest()
        {
            BallLogic.SetTable(_table);
            _ballLogic.Velocity = new Vector2(0, -1);
            _ballLogic.setPosition();
            Assert.AreEqual(50, _ballLogic.X);
            Assert.AreEqual(49, _ballLogic.Y);
        }
    }
}