using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
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
            var json = await ApiClient.Http.GetStringAsync("camara/admin");
            return JsonConvert.DeserializeObject<List<Models.Camera>>(json);
        }

        /// <summary>
        /// Envía una petición PATCH para cambiar el estado de la cámara (ACTIVA/INACTIVA).
        /// </summary>
        public static async Task<bool> SetCameraStateAsync(int id, string nuevoEstado)
        {
            try
            {
                // Construimos la URL: camara/1/INACTIVA
                var url = $"camara/{id}/{nuevoEstado}";

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);

                var response = await ApiClient.Http.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> DeleteCameraAsync(int id)
        {
            try
            {
                // Assuming ApiClient handles the base URL and Auth Token
                var response = await ApiClient.Http.DeleteAsync($"/camara/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting camera: {ex.Message}");
                return false;
            }
        }
    }
}
