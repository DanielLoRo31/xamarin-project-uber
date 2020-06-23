using System;
using System.Collections.Generic;
using System.Text;

namespace Proyect_U.Models
{
    public class TripModel
    {
        public int Id { get; set; }
        public int IdDriver { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public string OriginAddress { get; set; }
        public string OriginCoordinates { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationCoordinates { get; set; }
        public int Status { get; set; }
        public string Route { get; set; }
    }
}
