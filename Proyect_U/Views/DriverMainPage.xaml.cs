
using Proyect_U.Models;
using Proyect_U.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        MenuPages.Add(id, new NavigationPage(new PetMapPage(user)));
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