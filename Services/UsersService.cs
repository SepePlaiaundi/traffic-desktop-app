using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Usamos Newtonsoft para todo el archivo
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Services
{
    public static class UsersService
    {
        // Obtener todos los usuarios
        public static async Task<List<User>> GetUsersAsync()
        {
            // Usamos ApiClient.Http
            var json = await ApiClient.Http.GetStringAsync("users/all");
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        // Actualizar usuario
        public static async Task<bool> UpdateUserAsync(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await ApiClient.Http.PutAsync("users/update", content);
            return response.IsSuccessStatusCode;
        }


    }
}