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
    public partial class DriverMainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public DriverMainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Inicio, (NavigationPage)Detail);
        }


        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Inicio:  /*Iniciar un viaje entrys y boton */
                        MenuPages.Add(id, new NavigationPage(new DetailTripPage()));
                        break;
                    case (int)MenuItemType.Actualizar:  /*Actualizar Informacion del conductor*/
                        MenuPages.Add(id, new NavigationPage(new DetailTripPage()));
                        break;
                    case (int)MenuItemType.Mapa:   /*Mostrar Tres puntos de Apptrips, Inicio,final,Conductor, boton de terminar viaje*/
                        MenuPages.Add(id, new NavigationPage(new DetailTripPage()));
                        break;
                }
                
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

    }
}