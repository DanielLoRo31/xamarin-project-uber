using Proyect_U.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyect_U.Views
{
    [DesignTimeVisible(false)]
    public partial class DriverMenuPage : ContentPage
    {
        DriverMainPage RootPage { get => Application.Current.MainPage.Navigation.ModalStack[0] as DriverMainPage; }
        List<HomeMenuItem> menuItems;
        public DriverMenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Inicio, Title="Iniciar Viaje" },
                 new HomeMenuItem {Id = MenuItemType.Actualizar, Title="Actualizar Datos" },
                 new HomeMenuItem {Id = MenuItemType.Mapa, Title="Ver Mapa" },
                 new HomeMenuItem {Id = MenuItemType.Salir, Title="Salir" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                if (id != 3)
                {
                    await RootPage.NavigateFromMenu(id);
                } else
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                
            };
        }
    }
}