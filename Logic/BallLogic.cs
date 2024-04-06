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
            _ball.Position += new Vector2(_ball.Velocity.X * _ball.Speed, _ball.Velocity.Y * _ball.Speed);
            if (_ball.Position.X < 0 || _ball.Position.X > _table.Width - 25)
                _ball.Velocity *= -Vector2.UnitX;

            if (_ball.Position.Y < 0 || _ball.Position.Y > _table.Height - 25)
                _ball.Velocity *= -Vector2.UnitY;

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
