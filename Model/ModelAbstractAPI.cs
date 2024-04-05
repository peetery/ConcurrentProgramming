using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class ModelAbstractAPI
    {
        public static ModelAbstractAPI CreateModelAPI(LogicAbstractAPI logicAPI = default(LogicAbstractAPI))
        {
            return new ModelAPI(logicAPI);
        }

        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract ObservableCollection<BallLogic> createBalls(int amount, int radius);
        public abstract void StartSimulation();
        public abstract void StopSimulation();
    }
}