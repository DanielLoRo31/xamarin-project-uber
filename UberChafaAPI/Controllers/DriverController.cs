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

        [HttpPost]
        public ApiResponse Post([FromBody] DriverModel value)
        {
            return value.Insert();
        }

        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] DriverModel value)
        {
            return value.Update(id);
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new DriverModel().Delete(id);
        }
    }
}
