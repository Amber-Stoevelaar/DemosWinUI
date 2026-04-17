using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Media.Imaging;
using sampleMigration.Models;
using sampleMigration.Services.Logging;
using sampleMigration.Services.Messenger.Messages;
using System;

namespace sampleMigration.ViewModels
{
    public class PictureModuleViewModel : MyViewModelBase
    {
        private BitmapImage _image;
        private Theme _theme;

        public PictureModuleViewModel()
        { }

        public BitmapImage Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            var loggingService = Ioc.Default.GetService<ILoggingService>();

            _theme = Messenger.Send<ThemeRequestMessage>();
            loggingService.Log($"PictureModule requested theme and received {_theme.Name}.");
            UpdatePicture(_theme.Name);

            Messenger.Register<ThemeChangedMessage>(this, (r, m) =>
            {
                loggingService.Log($"PictureModule received change to {m.Value.Name}.");

                UpdatePicture(m.Value.Name);
            });
        }

        private void UpdatePicture(string themeName)
        {
            if (themeName == "Red")
            {
                Image = new BitmapImage(new Uri("ms-appx:///assets/inspectorspacetime.jpg"));
            }
            else
            {
                Image = new BitmapImage(new Uri("ms-appx:///assets/doctorwho.png"));
            }
        }
    }
}
