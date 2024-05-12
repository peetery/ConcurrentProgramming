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

        public Vector2 Position
        {
            get { return _ball.Position; }
        }

        public int Radius
        {
            get { return _ball.Radius; }
        }

        public float Mass
        {
            get { return _ball.Mass; }
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
            Vector2 newPosition = _ball.changePosition(_table);
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
        
        public bool ballCollision(BallLogic other)
        {
            Vector2 distance = new Vector2(other.X, other.Y) - new Vector2(this.X, this.Y);
            float seperationDistance = 1;
            float radiusSum = (other.Radius / 2) + (this.Radius / 2) + seperationDistance;
            return distance.LengthSquared() <= radiusSum * radiusSum;
        }

        public void handleBallColission(BallLogic other)
        {
            Vector2 collisionNormal = Vector2.Normalize(new Vector2(other.X, other.Y) - new Vector2(this.X, this.Y));
            Vector2 relativeVelocity = other.Velocity - this.Velocity;
            float collisionPulse = 2 * this.Mass * other.Mass * Vector2.Dot(relativeVelocity, collisionNormal) / (this.Mass + other.Mass);
            this.Velocity += collisionPulse / this.Mass * collisionNormal;
            other.Velocity -= collisionPulse / other.Mass * collisionNormal;
        }
    }
}
