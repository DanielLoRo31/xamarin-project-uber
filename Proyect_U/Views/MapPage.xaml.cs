using Proyect_U.Models;
using Proyect_U.Services;
using Proyect_U.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ZGAF_DELR_EXAMEN_2P.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        TripModel trip;
        static bool timerFlag;
        public MapPage(UserModel userSelected, TripModel tripSelected)
        {
            
            InitializeComponent();
            //Application.Current.MainPage.DisplayAlert("¡AAAAAAAA!", "AAAAAAAAAAAAAAAAAAAA", "Ok");
            trip = tripSelected;

            MapPet.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(Double.Parse(userSelected.CurrentLocation.Latitude), Double.Parse(userSelected.CurrentLocation.Longitude)),
                    Distance.FromMiles(.5)
            ));

            string imagePath = new ImageService().SaveImageFromBase64(userSelected.Picture, userSelected.Id);
            userSelected.PicturePath = imagePath;
            MapPet.User = userSelected;
            MapPet.Trip = tripSelected;

            MapPet.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = userSelected.Name,
                    Position = new Position(Double.Parse(userSelected.CurrentLocation.Latitude), Double.Parse(userSelected.CurrentLocation.Longitude))
                }
            );

            Name.Text = userSelected.Name;
            Age.Text = userSelected.LicensePlate;
            Notes.Text = userSelected.Password;
            timerFlag = true;
            TimerEventProcessor(userSelected);
        }

        static string position;
        // This is the method to run when the timer is raised.
        private static void TimerEventProcessor(UserModel userSelected)
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                DriverMainPage.GetInstance().UpdateUserLocation();
                return timerFlag; // True = Repeat again, False = Stop the timer
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            trip.Status = 2;
            trip.FinalDate = DateTime.Now;
            timerFlag = false;
            DriverMainPage.GetInstance().UdpdateActualTrip(trip, true);

        }


        
    }
}