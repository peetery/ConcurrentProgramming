using Logic;
using Model;
using System.Collections.ObjectModel;
using ViewModel;

namespace ViewModelTest
{
    public class MainWindowViewModelTests
    {
        private class ModelMockClass : ModelAbstractAPI
        {
            public int StartSimulationCallCount { get; set; }
            public int StopSimulationCallCount { get; set; }
            public int CreateBallsCallCount { get; set; }
            public override int Width => 500;
            public override int Height => 500;

            public override ObservableCollection<BallLogic> createBalls(int amount, int radius)
            {
                CreateBallsCallCount++;
                return new ObservableCollection<BallLogic>();
            }

            public override void StartSimulation()
            {
                StartSimulationCallCount++;
            }

            public override void StopSimulation()
            {
                StopSimulationCallCount++;
            }
        }

        [Test]
        public void CreateHandler_StartsSimulationTest()
        {
            var mockModel = new ModelMockClass();
            var viewModel = new MainWindowViewModel(mockModel);
            viewModel.ballsAmount = 5;

            viewModel.CreateHandler();

            Assert.AreEqual(1, mockModel.CreateBallsCallCount);
            Assert.AreEqual(1, mockModel.StartSimulationCallCount);
        }

        [Test]
        public void StopHandler_StopsSimulationTest()
        {
            var mockModel = new ModelMockClass();
            var viewModel = new MainWindowViewModel(mockModel);

            viewModel.StopHandler();

            Assert.AreEqual(1, mockModel.StopSimulationCallCount);
        }

        [Test]
        public void BallsGroup_Setter_RaisesPropertyChangedEventTest()
        {
            var mockModel = new ModelMockClass();
            var viewModel = new MainWindowViewModel(mockModel);

            bool eventRaised = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ballsGroup")
                    eventRaised = true;
            };

            viewModel.ballsGroup = new ObservableCollection<BallLogic>();
            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void BallsAmount_Setter_RaisesPropertyChangedEventTest()
        {
            var mockModel = new ModelMockClass();
            var viewModel = new MainWindowViewModel(mockModel);

            bool eventRaised = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "ballsAmount")
                    eventRaised = true;
            };

            viewModel.ballsAmount = 10;
            Assert.IsTrue(eventRaised);
        }
    }
}