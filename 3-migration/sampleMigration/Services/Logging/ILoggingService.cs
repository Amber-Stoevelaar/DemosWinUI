using System.Threading.Tasks;

namespace sampleMigration.Services.Logging
{
    public interface ILoggingService
    {
        Task Log(string message);
    }
}
