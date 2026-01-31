using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TrafficDesktopApp.Services
{
    /// <summary>
    /// Cliente HTTP centralizado para toda la aplicación.
    /// </summary>
    public static class ApiClient
    {
        public static readonly HttpClient Http = new HttpClient
        {
            BaseAddress = new Uri("http://127.0.0.1:8080/")
        };

        static ApiClient()
        {
            // Configuramos para que siempre pida y reciba JSON
            Http.DefaultRequestHeaders.Accept.Clear();
            Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Configura el token de seguridad (Bearer) en todas las peticiones futuras.
        /// </summary>
        public static void SetAuthToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Http.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
