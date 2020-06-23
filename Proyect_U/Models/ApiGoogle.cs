using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_U
{
    public class ApiGoogle
    {
        public Results []results;
        public string status { get; set; }

    }
    public class Results
    {
        public Geometry geometry;

    }
    public class Geometry
    {
        public Location location;
    }
    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
