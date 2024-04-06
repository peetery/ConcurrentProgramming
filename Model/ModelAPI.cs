using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public class ModelAPI : ModelAbstractAPI
    {
        private readonly LogicAbstractAPI _logicLayer;

        public ModelAPI() : this(LogicAbstractAPI.CreateAPI()) { }

        public ModelAPI(LogicAbstractAPI logicLayer)
        {
            _logicLayer = logicLayer ?? LogicAbstractAPI.CreateAPI();
        }
        public override int Width => _logicLayer.table.Width;
        public override int Height => _logicLayer.table.Height;
        public override ObservableCollection<BallLogic> createBalls(int amount, int radius)
        {
           _logicLayer.createBalls(amount, radius);
           return _logicLayer.balls;
        }
        public override void StartSimulation()
        {
            _logicLayer.RunSimulation();
        }
        public override void StopSimulation()
        {
            _logicLayer.StopSimulation();
        }

    }
}