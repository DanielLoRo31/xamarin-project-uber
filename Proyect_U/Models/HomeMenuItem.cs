using System;
using System.Collections.Generic;
using System.Text;

namespace Proyect_U.Models
{
    public enum MenuItemType
    {
        Inicio,
        Actualizar,
        Mapa,
        Salir
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
