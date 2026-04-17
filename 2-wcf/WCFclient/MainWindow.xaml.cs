using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ServiceReference1;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WCFclient
{
    public sealed partial class MainWindow : Window
    {

        private readonly ObservableCollection<string> _results = new();

        public MainWindow()
        {
            InitializeComponent();
            ResultsListView.ItemsSource = _results;
        }

        private Service1Client CreateClient()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress(
                "http://localhost:54502/Service1.svc");

            return new Service1Client(binding, endpoint);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(InputTextBox.Text, out int value))
            {
                InputTextBox.Text = "Please enter a valid number.";
                return;
            }

            var client = CreateClient();

            try
            {
                int input = int.Parse(InputTextBox.Text);
                var result = await client.GetDataAsync(input);
                
                _results.Add(result);

                

                //rootGrid.Children.Add(ResultsListView);
            }
            finally
            {
                try
                {
                    await client.CloseAsync();
                }
                catch
                {
                    client.Abort();
                }
            }
        }
    }
}
