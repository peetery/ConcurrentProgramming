using System;
using System.Numerics;

namespace Data
{
    internal class DataLayer : DataAPI
    {
        private static readonly SemaphoreSlim _fileSemaphore = new SemaphoreSlim(1, 1);

        public DataLayer() { }

        public override async Task LogDiagnosticDataAsync(string data)
        {
            string filePath = "diagnostic_log.json";
            await _fileSemaphore.WaitAsync();
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync(data);
                }
            }
            finally
            {
                _fileSemaphore.Release();
            }
        }
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

        public abstract Task LogDiagnosticDataAsync(string data);
    }
}