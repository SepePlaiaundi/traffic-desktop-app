using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Services
{
    /// <summary>
    /// Servicio para gestionar los recursos (fuentes de información) de las incidencias.
    /// </summary>
    public class RecursoService
    {
        /// <summary>
        /// Obtiene la lista de todos los recursos disponibles en el servidor.
        /// </summary>
        /// <returns>Lista de objetos Recurso.</returns>
        public static async Task<List<Recurso>> GetAllAsync()
        {
            try
            {
                var response = await ApiClient.Http.GetAsync("recurso");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Recurso>>(json);
                }
                return new List<Recurso>();
            }
            catch (Exception)
            {
                return new List<Recurso>();
            }
        }
    }
}
