using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTest
{
    public class MockDataAPI : DataAPI
    {
        private readonly List<string> _loggedData = new List<string>();
        private readonly SemaphoreSlim _fileSemaphore = new SemaphoreSlim(1, 1);

        public override async Task LogDiagnosticDataAsync(string data)
        {
            await _fileSemaphore.WaitAsync();
            try
            {
                _loggedData.Add(data);
            }
            finally
            {
                _fileSemaphore.Release();
            }
        }

        public List<string> GetLoggedData() => _loggedData;
    }
}
