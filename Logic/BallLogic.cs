using Data;
using System.Numerics;

namespace Logic
{
    public class BallLogic
    {
        private Ball _ball;
        private static Table _table;

        public float X
        {
            get { return _ball.Position.X; }
        }

        public float Y
        {
            get { return _ball.Position.Y; }
        }

        public int Radius
        {
            get { return _ball.Radius; }
        }

        public BallLogic(Ball ball)
        {
            _ball = ball;
        }

        public static void SetTable(Table table)
        {
            _table = table;
        }

        public Vector2 Velocity
        {
            get 
            { 
                return _ball.Velocity; 
            }
            set 
            { 
                _ball.Velocity = value;
            }
        }
    }
}