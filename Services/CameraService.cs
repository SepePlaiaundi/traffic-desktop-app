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
    public static class CamerasService
    {
        public static async Task<List<CameraDto>> GetCamerasAsync()
        {
            var json = await ApiClient.Http.GetStringAsync("camara");
            var all = JsonConvert.DeserializeObject<List<CameraDto>>(json);

            return all.Take(5).ToList();
        }
    }
}
