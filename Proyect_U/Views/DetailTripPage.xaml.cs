using Proyect_U.Models;
using Proyect_U.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyect_U.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTripPage : ContentPage
    {
        public DetailTripPage()
        {
            InitializeComponent();
            BindingContext = new DetailTripViewModel();
        }
        public DetailTripPage(UserModel user)
        {
            InitializeComponent();
            BindingContext = new DetailTripViewModel(user);
        }
    }
}