using Plugin.Media;
using Proyect_U.Models;
using Proyect_U.Services;
using Proyect_U.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proyect_U.ViewModel
{
    public class SignInViewModel: BaseViewModel
    {
        bool opcion;

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        Command _RegisterCommand;
        public Command RegisterCommand => _RegisterCommand ?? (_RegisterCommand = new Command(RegisterAction));

        string _Title;
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }

        string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        string _Email;
        public string Email
        {
            get => _Email;
            set => SetProperty(ref _Email, value);
        }

        string _Password;
        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }

        string _PlateCar;
        public string PlateCar
        {
            get => _PlateCar;
            set => SetProperty(ref _PlateCar, value);
        }

        string _Image;
        public string Image
        {
            get => _Image;
            set => SetProperty(ref _Image, value);
        }

        public SignInViewModel(string s)
        {
            Title = s;
            opcion = true;
        }

        public SignInViewModel(UserModel u, string s)
        {
            Title = s;
            opcion = false;
        }

        private void RegisterAction()
        {
            if (opcion == true)
            {
                Application.Current.MainPage.Navigation.PopAsync();
            }
            
        }

        private async void SelectPictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("ERROR", ":( Seleccionar fotografia no esta disponible.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            Image = new ImageService().ConvertImageSourceToBase64(file.Path);


        }

        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            Image = new ImageService().ConvertImageSourceToBase64(file.Path);
        }

    }
}
