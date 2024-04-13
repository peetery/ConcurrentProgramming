using Data;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class BallLogic : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void setVelocity(Vector2 velocity)
        {
            _ball.Velocity = velocity;
        }

        public void setPosition()
        {
            Vector2 newPosition = _ball.Position + new Vector2(_ball.Velocity.X * _ball.Speed, _ball.Velocity.Y * _ball.Speed);

            if (newPosition.X < 0)
            {
                _ball.Velocity = new Vector2(-_ball.Velocity.X, _ball.Velocity.Y);
                newPosition.X = 0;
            }
            else if (newPosition.X > _table.Width - _ball.Radius)
            {
                _ball.Velocity = new Vector2(-_ball.Velocity.X, _ball.Velocity.Y);
                newPosition.X = _table.Width - _ball.Radius;
            }
            
            if (newPosition.Y < 0)
            {
                _ball.Velocity = new Vector2(_ball.Velocity.X, -_ball.Velocity.Y);
                newPosition.Y = 0;
            }
            else if (newPosition.Y > _table.Height - _ball.Radius)
            {
                _ball.Velocity = new Vector2(_ball.Velocity.X, -_ball.Velocity.Y);
                newPosition.Y = _table.Height - _ball.Radius;
            }

            _ball.Position = newPosition;
            RaisePropertyChanged(nameof(X));
            RaisePropertyChanged(nameof(Y));
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
