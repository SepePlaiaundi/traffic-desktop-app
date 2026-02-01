using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrafficDesktopApp.Services
{
    /// <summary>
    /// Servicio para gestionar las incidencias con la API de tráfico.
    /// </summary>
    public class IncidenceService
    {
        /// <summary>
        /// Crea una nueva incidencia enviándola al servidor.
        /// </summary>
        /// <param name="incidence">El objeto de incidencia con los datos a guardar.</param>
        /// <returns>True si la creación fue exitosa; de lo contrario, False.</returns>
        public static async Task<bool> CreateIncidenceAsync(Models.Incidence incidence)
        {
            var json = JsonConvert.SerializeObject(incidence);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await ApiClient.Http.PostAsync("incidencia", content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Obtiene la lista completa de incidencias registradas.
        /// </summary>
        /// <returns>Una lista de objetos de tipo Incidence.</returns>
        public static async Task<List<Models.Incidence>> GetIncidencesAsync()
        {
            var json = await ApiClient.Http.GetStringAsync("incidencia");
            return JsonConvert.DeserializeObject<List<Models.Incidence>>(json);
        }
    }
}
