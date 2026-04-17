using sampleMigration.Models;
using sampleMigration.Services.Messenger.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace sampleMigration.ViewModels
{
    public class ShellViewModel : ObservableRecipient
    {
        private Theme _theme = Theme.Default;

        public ShellViewModel()
        {
            Messenger.Register<ThemeRequestMessage>(this, (r, m) =>
            {
                m.Reply(_theme);
            });

            Messenger.Register<ThemeChangedMessage>(this, (r, m) =>
            {
                _theme = m.Value;
            });
        }
    }
}
