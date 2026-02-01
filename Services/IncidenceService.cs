using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrafficDesktopApp.Services
{
    public class IncidenceService
    {
        public static async Task<bool> CreateIncidenceAsync(Models.Incidence incidence)
        {
            var json = JsonConvert.SerializeObject(incidence);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await ApiClient.Http.PostAsync("incidencia", content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<List<Models.Incidence>> GetIncidencesAsync()
        {
            var json = await ApiClient.Http.GetStringAsync("incidencia");
            return JsonConvert.DeserializeObject<List<Models.Incidence>>(json);
        }
    }
}
