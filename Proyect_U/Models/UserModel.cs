using System;
using System.Collections.Generic;
using System.Text;

namespace Proyect_U.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public string Picture { get; set; }
        public string PicturePath { get; set; }
        public PositionModel CurrentLocation { get; set; }
        public string Password { get; set; }

        public class PositionModel
        {
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }
    }
}
