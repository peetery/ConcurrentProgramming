using System;
using System.Numerics;

namespace Data
{
    internal class DataLayer : DataAPI
    {
        public DataLayer() { }
    }

    public abstract class DataAPI
    {
        public static DataAPI createDataAPI()
        {
            return new DataLayer();
        }

        public virtual Ball getBallData(int radius, Vector2 position, Vector2 velocity, float mass)
        {
            return new Ball(radius, position, velocity, mass);
        }

        public virtual Table getTableData(int width, int height)
        {
            return new Table(width, height);
        }
    }
}