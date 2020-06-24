
using Proyect_U.Models;
using Proyect_U.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZGAF_DELR_EXAMEN_2P.Views;

namespace Proyect_U.Views
{
    [DesignTimeVisible(false)]
    public partial class DriverMainPage : MasterDetailPage
    {
        static DriverMainPage instance;
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        UserModel user;
        TripModel trip;
        public DriverMainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Inicio, (NavigationPage)Detail);
        }

        public DriverMainPage(UserModel u)
        {
            instance = this;
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            //MenuPages.Add((int)MenuItemType.Inicio, (NavigationPage)Detail);
            user = u;

            this.TakeActualTrip();

            this.NavigateFromMenu(0);
        }

        public static DriverMainPage GetInstance()
        {
            if (instance == null) instance = new DriverMainPage();
            return instance;
        }

        public TripModel GetActualTrip()
        {
            return this.trip;
        }

        public async void UdpdateActualTrip(TripModel t, bool type)
        {
            ApiResponse response = await new ApiService().PutDataAsync($"trip", t);
            if (response == null || !response.IsSuccess)
            {
                //await Application.Current.MainPage.DisplayAlert("No viaje", response.Message, "Ok");
                return;
            }
            await Application.Current.MainPage.DisplayAlert("¡Éxito!", response.Message, "Ok");
            if (type == false)
            {
                this.trip = t;
            } else
            {
                this.trip = null;
                this.NavigateFromMenu(0);
            }
            
        }


        private async void TakeActualTrip()
        {
            ApiResponse response = await new ApiService().GetTripByIdAsync<TripModel>($"trip/actual/{user.Id}");
            if (response == null || !response.IsSuccess)
            {
                //await Application.Current.MainPage.DisplayAlert("No viaje", response.Message, "Ok");
                return;
            }
            await Application.Current.MainPage.DisplayAlert("¡Estás en viaje!", response.Message, "Ok");
            trip = (TripModel)response.Result;
            this.NavigateFromMenu(2);
        }

        public async void UpdateUserLocation()
        {
            string position = await GetActualPosition();
            user.CurrentLocation.Latitude = position.Split(' ')[0];
            user.CurrentLocation.Longitude = position.Split(' ')[1];

            ApiResponse response = await new ApiService().PutDataAsync("driver", user);
            if (response == null || !response.IsSuccess)
            {
                //await Application.Current.MainPage.DisplayAlert("No viaje", response.Message, "Ok");
                return;
            }
           
            this.NavigateFromMenu(2);
        }

        private async Task<string> GetActualPosition()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    return location.Latitude.ToString() + " " + location.Longitude.ToString();

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



        public async Task NavigateFromMenu(int id)
        {
            
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Inicio:  /*Iniciar un viaje entrys y boton */
                        MenuPages.Add(id, new NavigationPage(new DetailTripPage(user)));
                        break;
                    case (int)MenuItemType.Actualizar:  /*Actualizar Informacion del conductor*/
                        MenuPages.Add(id, new NavigationPage(new SignInPage(user)));
                        break;
                    case (int)MenuItemType.Mapa:   /*Mostrar Tres puntos de Apptrips, Inicio,final,Conductor, boton de terminar viaje*/
                        MenuPages.Add(id, new NavigationPage(new MapPage(user, trip)));
                        break;
                }
                
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

    }
}