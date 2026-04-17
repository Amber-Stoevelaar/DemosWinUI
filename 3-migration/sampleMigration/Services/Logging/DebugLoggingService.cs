using System.Diagnostics;
using System.Threading.Tasks;

namespace sampleMigration.Services.Logging
{
    public class DebugLoggingService : ILoggingService
    {
        public Task Log(string message)
        {
            Debug.WriteLine(message);
            return Task.FromResult(0);
        }
    }
}
