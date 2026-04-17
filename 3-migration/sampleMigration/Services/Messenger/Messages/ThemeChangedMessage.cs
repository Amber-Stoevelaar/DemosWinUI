using CommunityToolkit.Mvvm.Messaging.Messages;
using sampleMigration.Models;

namespace sampleMigration.Services.Messenger.Messages
{
    public class ThemeChangedMessage : ValueChangedMessage<Theme>
    {
        public ThemeChangedMessage(Theme value) : base(value)
        {
        }
    }
}
