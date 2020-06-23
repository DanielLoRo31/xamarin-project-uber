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
    public class DetailTripViewModel: BaseViewModel
    {
        UserModel user;
        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        

        string _InitialAddress;
        public string InitialAddress
        {
            get => _InitialAddress;
            set => SetProperty(ref _InitialAddress, value);
        }

        string _FinalAddresse;
        public string FinalAddress
        {
            get => _FinalAddresse;
            set => SetProperty(ref _FinalAddresse, value);
        }

        public DetailTripViewModel(UserModel u)
        {
            user = u;
        }

        public DetailTripViewModel() { }

        private async Task<string> GetPosition(string direction)
        {
            ApiResponse response = await new LocationService().GetDataAsync<ApiGoogle>(direction);
            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Ubicación", "Especifique mejor la Ubicación", "Ok");
                return null;
            }

            ApiGoogle apiResponse = (ApiGoogle)response.Result;

            return apiResponse.results[0].geometry.location.lat.ToString() + " " + apiResponse.results[0].geometry.location.lng.ToString();
            
        }

        private async Task<string> GetActualPosition()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    return location.Latitude.ToString() + " " + location.Longitude.ToString() + ",";

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


        private async void GetLocationAction()
        {
            TripModel trip = new TripModel
            {
                IdDriver = user.Id,
                OriginAddress = this.InitialAddress,
                OriginCoordinates = await GetPosition(this.InitialAddress),
                DestinationAddress = this.FinalAddress,
                DestinationCoordinates = await GetPosition(this.FinalAddress),
                Status = 1,
                Route = await GetActualPosition()
            };

            ApiResponse response = await new ApiService().PostDataAsync("trip", trip);
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
            DriverMainPage.GetInstance().UdpdateActualTrip(trip, false);
            await DriverMainPage.GetInstance().NavigateFromMenu(2);

        }
    }
}
