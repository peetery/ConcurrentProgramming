using System;

namespace Data
{
    public class Ball
    {
        public int Radius;
        public double X;
        public double Y;

        private int _Speed;
        private bool _isAlive;

        public Ball(int radius, double x, double y)
        {
            this.Radius = radius;
            this.X = x;
            this.Y = y;
            this._Speed = 100;
            this._isAlive = true;
        }
    }
}