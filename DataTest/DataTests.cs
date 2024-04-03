using Data;
using System.Numerics;

namespace DataTest
{
    public class DataTests
    {
        [Test]
        public void ballConstructorTest()
        {
            var radius = 10;
            var position = new Vector2(5, 5);
            var velocity = new Vector2(1, 1);

            Ball ball = new Ball(radius, position, velocity);

            Assert.AreEqual(radius, ball.Radius);
            Assert.AreEqual(position, ball.Position);
            Assert.AreEqual(velocity, ball.Velocity);
        }

        [Test]
        public void tableConstructorTest()
        {
            var width = 100;
            var height = 200;

            Table table = new Table(width, height);

            Assert.AreEqual(width, table.Width);
            Assert.AreEqual(height, table.Height);
        }

        [Test]
        public void getBallDataTest()
        {
            DataAPI dataAPI = DataAPI.createDataAPI();
            var radius = 10;
            var position = new Vector2(5, 5);
            var velocity = new Vector2(1, 1);
            var ballData = dataAPI.getBallData(radius, position, velocity);

            Assert.AreEqual(position, ballData.Position);
            Assert.AreEqual(velocity, ballData.Velocity);
            Assert.AreEqual(radius, ballData.Radius);
        }

        [Test]
        public void getTableDataTest()
        {
            DataAPI dataAPI = DataAPI.createDataAPI();
            var width = 500;
            var height = 500;
            var tableData = dataAPI.getTableData(width, height);

            Assert.AreEqual (width, tableData.Width);
            Assert.AreEqual(height , tableData.Height);
        }
    }
}