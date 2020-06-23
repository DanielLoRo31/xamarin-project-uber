using Proyect_U.Models;
using Proyect_U.ViewModels;
using Proyect_U.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Proyect_U.ViewModel
{
    public class MainPageViewModel: BaseViewModel
    {
        Command _LogInCommand;

        public Command LogInCommand => _LogInCommand ?? (_LogInCommand = new Command(LogInAction));

        Command _SignInCommand;

        public Command SignInCommand => _SignInCommand ?? (_SignInCommand = new Command(SignInAction));


        string _Email;
        public string Email
        {
            get => _Email;
            set => SetProperty(ref _Email, value);
        }

        string _Password;
        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }


        private void LogInAction()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new DriverMainPage(new UserModel()));
        }

        private void SignInAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SignInPage());
        }
    }
}
