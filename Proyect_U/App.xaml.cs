using Proyect_U.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyect_U
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var nav = new NavigationPage(new MainPage());
            MainPage = nav;

            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
