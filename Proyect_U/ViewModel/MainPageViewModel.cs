using Proyect_U.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proyect_U.ViewModel
{
    public class MainPageViewModel
    {
        Command _LogInCommand;

        public Command LogInCommand => _LogInCommand ?? (_LogInCommand = new Command(LogInAction));

        private void LogInAction()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new DriverMainPage());
        }
    }
}
