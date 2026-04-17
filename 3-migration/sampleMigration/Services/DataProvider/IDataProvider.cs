using sampleMigration.Models;

namespace sampleMigration.Services
{
    public interface IDataProvider
    {
        string Description { get; }
        SuperHero SuperHero();
    }
}
