using Proyect_U.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace AppTrips.UWP
{
    public sealed partial class MarkerWindow : UserControl
    {
        public MarkerWindow(UserModel user)
        {
            this.InitializeComponent();

            MarkerWindowImage.Source = new BitmapImage(new Uri(user.Picture));
            MarkerWindowTitle.Text = user.Name;
            MarkerWindowNotes.Text = user.LicensePlate;
        }
    }
}
