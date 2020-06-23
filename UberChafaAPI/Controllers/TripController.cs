using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UberChafaAPI.Models;

namespace UberChafaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        [HttpGet]
        public ObservableCollection<TripModel> Get()
        {
            return new TripModel().GetAll();
        }

        [HttpGet("{id}", Name = "Select")]
        public TripModel Get(int id)
        {
            return new TripModel().Get(id);
        }

        [HttpGet("actual/{id}", Name = "Actual")]
        public TripModel GetActual(int id)
        {
            return new TripModel().GetActualTrip(id);
        }

        [HttpPost]
        public ApiResponse Post([FromBody] TripModel value)
        {
            return value.Insert();
        }

        [HttpPut]
        public ApiResponse Put([FromBody] TripModel value)
        {
            return value.Update();
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new TripModel().Delete(id);
        }
    }
}
