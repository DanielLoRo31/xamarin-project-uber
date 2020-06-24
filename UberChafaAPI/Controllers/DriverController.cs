using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using UberChafaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UberChafaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        [HttpGet]
        public ObservableCollection<DriverModel> Get()
        {
            return new DriverModel().GetAll();
        }

        [HttpGet("{id}", Name = "Get")]
        public DriverModel Get(int id)
        {
            return new DriverModel().Get(id);
        }

        [HttpPost("login")]
        public DriverModel GetLogin([FromBody] DriverModel value)
        {
            return value.GetLogin();
        }

        [HttpPost]
        public ApiResponse Post([FromBody] DriverModel value)
        {
            return value.Insert();
        }

        [HttpPut]
        public ApiResponse Put([FromBody] DriverModel value)
        {
            return value.Update();
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new DriverModel().DeleteUserAndTrips(id);
        }
    }
}
