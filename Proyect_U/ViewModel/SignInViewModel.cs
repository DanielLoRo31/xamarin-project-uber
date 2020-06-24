using Plugin.Media;
using Proyect_U.Models;
using Proyect_U.Services;
using Proyect_U.ViewModels;
using Proyect_U.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Proyect_U.Models.UserModel;

namespace Proyect_U.ViewModel
{
    public class SignInViewModel: BaseViewModel
    {
        int id;
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
            id = u.Id;
            Title = s;
            Name = u.Name;
            Password = u.Password;
            PlateCar = u.LicensePlate;
            Image = u.Picture;
            opcion = false;

        }

        

        private async void RegisterAction()
        {
            if (opcion == true)
            {
                ApiResponse response = await new ApiService().PostDataAsync("driver", new UserModel
                {
                    Name = this.Name,
                    LicensePlate =  this.PlateCar,
                    Picture = this.Image,
                    CurrentLocation = await GetLocationAction(),
                    Password = this.Password
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Uber Chafa", "Error al crear el usuario", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Uber Chafa", response.Message, "Ok");
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("Uber Chafa", response.Message, "Ok");
                Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                ApiResponse response = await new ApiService().PutDataAsync("driver", new UserModel
                {
                    Id = this.id,
                    Name = this.Name,
                    LicensePlate = this.PlateCar,
                    Picture = this.Image,
                    CurrentLocation = await GetLocationAction(),
                    Password = this.Password
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Uber Chafa", "Error al crear el usuario", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Uber Chafa", response.Message, "Ok");
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("Uber Chafa", response.Message, "Ok");
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

        private async Task<PositionModel> GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    return new PositionModel
                    {
                        Latitude = location.Latitude.ToString(),
                        Longitude = location.Longitude.ToString()
                    };
                    
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                return null;
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            return null;
        }

    }
}
