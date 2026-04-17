using sampleMigration.Models;

namespace sampleMigration.Services
{
    public class RedDataProvider : IDataProvider
    {
        public string Description => "Red Phonebox";

        public SuperHero SuperHero()
        {
            return new SuperHero
            {
                Name = "Inspector Spacetime",
                Nemesis = "The Blorgons",
                Tool = "Quantum Spanner"
            };
        }
    }
}
