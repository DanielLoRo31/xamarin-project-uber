using Proyect_U.Models;
using Proyect_U.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppTrips.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripMapPage : ContentPage
    {
        public TripMapPage(UserModel user, TripModel trip)
        {
            InitializeComponent();

            MapTrip.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position (user.Latitude, user.Longitude),
                    Distance.FromMiles(.5)
            ));




            user.Picture = new ImageService().SaveImageFromBase64(user.Picture, user.Id);
            MapTrip.User = user;
            MapTrip.Trip = trip;

            MapTrip.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = user.Name,
                    Position = new Position(user.Latitude, user.Longitude)
                }
            );

            Name.Text = user.Name;
            Date.Text = trip.InitialDate.ToShortDateString();
            License.Text = $"N. Placas {user.LicensePlate}";
            Destination.Text = user.DestinationAddress;
        }
    }
}