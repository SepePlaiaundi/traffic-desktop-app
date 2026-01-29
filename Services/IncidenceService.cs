using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrafficDesktopApp.Services
{
    public class IncidenceService
    {
        public static async Task<List<Models.Incidence>> GetIncidencesAsync()
        {
            var json = await ApiClient.Http.GetStringAsync("incidencia");
            return JsonConvert.DeserializeObject<List<Models.Incidence>>(json);
        }
    }
}
