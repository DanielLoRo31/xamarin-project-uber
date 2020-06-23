using Newtonsoft.Json;
using Proyect_U.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proyect_U.Services
{
    public class LocationService
    {
        private string ApiUrl = "https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyCmtLt6Q2wnzCeG4jOaq3N6MQK5Rg9zJQ4&address=";

        public async Task<ApiResponse> GetDataAsync<T>(string direction)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new System.Uri(ApiUrl + direction)
                };
                var response = await client.GetAsync("");
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var data = JsonConvert.DeserializeObject<T>(result);
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = data
                };
            }
            catch (System.Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

    }
}
