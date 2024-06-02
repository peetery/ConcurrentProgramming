using Data;
using System.Numerics;

namespace DataTest
{
    public class DataTests
    {
        [Test]
        public void createAPITest()
        {
            DataAPI dataAPI = DataAPI.createDataAPI();
            Assert.IsNotNull(dataAPI);
        }

        [Test]
        public void getBallDataTest()
        {
            DataAPI dataAPI = DataAPI.createDataAPI();
            var mass = 5f;
            var radius = 10;
            var position = new Vector2(5, 5);
            var velocity = new Vector2(1, 1);
            var ballData = dataAPI.getBallData(radius, position, velocity, mass);

            Assert.AreEqual(position, ballData.Position);
            Assert.AreEqual(velocity, ballData.Velocity);
            Assert.AreEqual(radius, ballData.Radius);
            Assert.AreEqual(mass, ballData.Mass);
            Assert.AreEqual(100, ballData.Speed);
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

        [Test]
        public async Task LogDiagnosticDataAsyncTest()
        {
            var mockDataAPI = new MockDataAPI();
            string testData = "{\"Event\":\"Test\"}";

            await mockDataAPI.LogDiagnosticDataAsync(testData);

            List<string> loggedData = mockDataAPI.GetLoggedData();

            Assert.IsNotNull(loggedData);
            Assert.AreEqual(1, loggedData.Count);
            Assert.AreEqual(testData, loggedData.First());
        }
    }
}