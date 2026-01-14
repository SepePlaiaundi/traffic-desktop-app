using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TrafficDesktopApp.Services
{
    public static class ApiClient
    {
        public static readonly HttpClient Http = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080/")
        };

        static ApiClient()
        {
            Http.DefaultRequestHeaders.Accept.Clear();
            Http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }
    }
}
