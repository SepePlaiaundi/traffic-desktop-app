using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Newtonsoft.Json;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Services
{
    /// <summary>
    /// Servicio para la obtención de datos de cámaras de tráfico desde la API.
    /// </summary>
    public static class CamerasService
    {
        public static async Task<List<Models.Camera>> GetCamerasAsync()
        {
            var json = await ApiClient.Http.GetStringAsync("camara");
            return JsonConvert.DeserializeObject<List<Models.Camera>>(json);
        }
    }
}
