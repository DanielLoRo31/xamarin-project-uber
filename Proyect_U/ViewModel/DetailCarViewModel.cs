using Proyect_U.Models;
using Proyect_U.Services;
using Proyect_U.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Proyect_U.ViewModel
{
    public class DetailCarViewModel : BaseViewModel
    {

        ObservableCollection<UserModel> _Users;
        public ObservableCollection<UserModel> Users
        {
            get => _Users;
            set => SetProperty(ref _Users, value);
        }
        UserModel _UserSelected;
        public UserModel UserSelected
        {
            get => _UserSelected;
            set => SetProperty(ref _UserSelected, value);
        }
        Command _SelectCommand;
        public Command SelectCommand => _SelectCommand ?? (_SelectCommand = new Command(SelectUserAction));

        Command _DeleteCommand;
        public Command DeleteCommand => _DeleteCommand ?? (_DeleteCommand = new Command(DeleteUserAction));


        Command _LogOutCommand;

        public Command LogOutCommand => _LogOutCommand ?? (_LogOutCommand = new Command(LogOutAction));

        private async void LogOutAction()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void DeleteUserAction()
        {
            if (UserSelected != null)
            {
                ApiResponse response = await new ApiService().DeleteDataAsync("driver", UserSelected.Id);
                if (response == null || !response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("UberChafa", "Error al eliminar ese conductor", "Ok");
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("UberChafa", UserSelected.Name + " ha sido eliminado", "Ok");

                GetAllUsers();
                
            }
            
        }

        private async void SelectUserAction()
        {
            await Application.Current.MainPage.DisplayAlert("UberChafa", "Has seleccionado a: " + UserSelected.Name, "Ok");
        }

        

        public DetailCarViewModel()
        {
            GetAllUsers();
        }

        private async void GetAllUsers()
        {
            ApiResponse response = await new ApiService().GetDataAsync<UserModel>("driver");
            if (response == null || !response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("UberChafa", "Error al traer los conductores", "Ok");
                return;
            }
            Users = (ObservableCollection<UserModel>)response.Result;
        }

        
    }
}
