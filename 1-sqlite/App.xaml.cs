using Microsoft.UI.Xaml;
using SQLite_PoC.Data;
using SQLitePCL;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SQLite_PoC
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? _window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 

        // App.xaml.cs
        public App()
        {
            this.InitializeComponent();
            Batteries.Init();
        }

        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            await DataAccess.InitializeDatabase();
            _window = new MainWindow();
            _window.Activate();
        }
    }
}
