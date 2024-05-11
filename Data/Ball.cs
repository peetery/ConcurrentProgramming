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

        public float Mass { get; set; }

        public Ball(int radius, Vector2 position, Vector2 velocity, float mass)
        {
            this.Radius = radius;
            this.Position = position;
            this.Velocity = velocity;
            this.Mass = mass;
        }

        public Vector2 changePosition(Table table)
        {
            Vector2 newPosition = this.Position + new Vector2(this.Velocity.X * this.Speed, this.Velocity.Y * this.Speed);

            if (newPosition.X < 0)
            {
                this.Velocity = new Vector2(-this.Velocity.X, this.Velocity.Y);
                newPosition.X = 0;
            }
            else if (newPosition.X > table.Width - this.Radius)
            {
                this.Velocity = new Vector2(-this.Velocity.X, this.Velocity.Y);
                newPosition.X = table.Width - this.Radius;
            }

            if (newPosition.Y < 0)
            {
                this.Velocity = new Vector2(this.Velocity.X, -this.Velocity.Y);
                newPosition.Y = 0;
            }
            else if (newPosition.Y > table.Height - this.Radius)
            {
                this.Velocity = new Vector2(this.Velocity.X, -this.Velocity.Y);
                newPosition.Y = table.Height - this.Radius;
            }

            return newPosition;
        }
    }
}