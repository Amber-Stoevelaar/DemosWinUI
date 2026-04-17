//using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace sampleMigration.Views
{
    public sealed partial class Shell : Page
    {
        public Shell()
        {
            InitializeComponent();
        }

        public UIElement TitleBarElement => AppTitleBar;

        private void NavigationView_SelectionChanged(
            WinUI.NavigationView sender,
            WinUI.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                NavigationView.Header = "Settings";
                return;
            }

            if (args.SelectedItemContainer is WinUI.NavigationViewItem item &&
                item.Tag is string pageTypeName)
            {
                var pageType = Type.GetType(pageTypeName);

                if (pageType != null)
                {
                    ContentFrame.Navigate(pageType, item.Content);
                    NavigationView.Header = item.Content;
                }
            }
        }
    }
}
