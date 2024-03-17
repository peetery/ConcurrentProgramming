using BasicApplication;

[assembly: Apartment(ApartmentState.STA)]

namespace BasicApplicationTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void AddTest()
        {
            var mainWindow = new MainWindow();
            int result = mainWindow.Add(2, 3);
            Assert.That(result, Is.EqualTo(5));
        }
    }
}