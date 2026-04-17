using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using sampleMigration.Services.Logging;

namespace sampleMigration.ViewModels
{
    public class MyViewModelBase : ObservableRecipient
    {
        public ILoggingService LoggingService => Ioc.Default.GetService<ILoggingService>();
    }
}
