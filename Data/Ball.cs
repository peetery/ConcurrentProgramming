using System;
using System.Numerics;

namespace Data
{
    public class Ball
    {
        public int Radius { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int Speed { get; set; } = 100;

        private bool _isAlive;

        public Ball(int radius, Vector2 position, Vector2 velocity)
        {
            this.Radius = radius;
            this.Position = position;
            this.Velocity = velocity;
            this._isAlive = true;
        }
    }
}