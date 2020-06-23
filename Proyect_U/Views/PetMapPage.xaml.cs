using Proyect_U.Models;
using Proyect_U.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ZGAF_DELR_EXAMEN_2P.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetMapPage : ContentPage
    {
        public PetMapPage(UserModel petSelected)
        {
            InitializeComponent();

            MapPet.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(Double.Parse(petSelected.CurrentLocation.Latitude), Double.Parse(petSelected.CurrentLocation.Longitude)),
                    Distance.FromMiles(.5)
            ));

            string imagePath = new ImageService().SaveImageFromBase64(petSelected.Picture, petSelected.Id);
            petSelected.Picture = imagePath;
            MapPet.User = petSelected;

            MapPet.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = petSelected.Name,
                    Position = new Position(Double.Parse(petSelected.CurrentLocation.Latitude), Double.Parse(petSelected.CurrentLocation.Longitude))
                }
            );
            
            Name.Text = petSelected.Name;
            Age.Text = petSelected.LicensePlate;
            Notes.Text = petSelected.Password;
        }
    }
}