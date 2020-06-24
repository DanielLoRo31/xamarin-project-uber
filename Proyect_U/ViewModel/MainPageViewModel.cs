using Proyect_U.Models;
using Proyect_U.Services;
using Proyect_U.ViewModels;
using Proyect_U.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Proyect_U.ViewModel
{
    public class MainPageViewModel: BaseViewModel
    {
        Command _LogInCommand;

        public Command LogInCommand => _LogInCommand ?? (_LogInCommand = new Command(LogInActionAsync));

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


        private async void LogInActionAsync()
        {
            ApiResponse response = await new ApiService().GetDataWithBodyAsync("driver/login", new UserModel
            {
                Name = this.Email,
                Password = this.Password
            });
            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("Uber Chafa", "Error al crear el usuario", "Ok");
                return;
            }
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Uber Chafa", response.Message, "Ok");
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Uber Chafa", response.Message, "Ok");

            this.Email = "";
            this.Password = "";

            if (((UserModel)response.Result).Name == "admin")
                await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new DetailCarPage()));
            else
                await Application.Current.MainPage.Navigation.PushModalAsync(new DriverMainPage(response.Result as UserModel));
        }

        

        private void SignInAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SignInPage());
        }
    }
}
