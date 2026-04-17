//using Microsoft.UI;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WinRT.Interop;

namespace sampleMigration
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;

            RootFrame.Navigated += RootFrame_Navigated;
            RootFrame.Navigate(typeof(Views.Shell));

            ConfigureTitleBarButtons();
        }

        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Views.Shell shell)
            {
                SetTitleBar(shell.TitleBarElement);
            }
        }

        private void ConfigureTitleBarButtons()
        {
            var appWindow = AppWindow.GetFromWindowId(
                Win32Interop.GetWindowIdFromWindow(
                    WindowNative.GetWindowHandle(this)));

            appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }
    }
}