using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateAPI()
        {
            return new LogicAPI();
        }
        public abstract Table table { get; }
        public abstract ObservableCollection<BallLogic> balls { get; }
        public abstract void RunSimulation();
        public abstract void StopSimulation();
        public abstract BallLogic createBall(int radius, Vector2 position);
        public abstract void createBalls(int amount, int radius);
        public abstract void deleteBalls();
    }
}