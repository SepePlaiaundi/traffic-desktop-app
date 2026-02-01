using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Services
{
    /// <summary>
    /// Servicio para la gestión de usuarios, roles y autenticación.
    /// </summary>
    public static class UsersService
    {
        // GET: /users/roles
        /// <summary>
        /// Obtiene la lista de roles disponibles en el sistema.
        /// </summary>
        /// <returns>Una lista de objetos RoleResponse.</returns>
        public static async Task<List<RoleResponse>> GetRolesAsync()
        {
            var json = await ApiClient.Http.GetStringAsync("users/roles");
            return JsonConvert.DeserializeObject<List<RoleResponse>>(json);
        }

        // GET: /users/all
        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados.
        /// </summary>
        /// <returns>Una lista de objetos User.</returns>
        public static async Task<List<User>> GetAllUsersAsync()
        {
            var json = await ApiClient.Http.GetStringAsync("users/all");
            return JsonConvert.DeserializeObject<List<User>>(json);
        }

        // PUT: /users/update
        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        /// <param name="request">Objeto con los datos de actualización.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public static async Task<bool> UpdateUserAsync(UserUpdateRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await ApiClient.Http.PutAsync("users/update", content);
            return response.IsSuccessStatusCode;
        }

        // POST: /users/register
        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="request">Objeto con los datos del nuevo usuario.</param>
        /// <returns>True si el registro fue exitoso.</returns>
        public static async Task<bool> CreateUserAsync(UserCreateRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await ApiClient.Http.PostAsync("users/register", content);
            return response.IsSuccessStatusCode;
        }

        // POST: /users/login
        /// <summary>
        /// Realiza el proceso de inicio de sesión.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>True si las credenciales son válidas y se obtuvo el token.</returns>
        public static async Task<bool> LoginAsync(string email, string password)
        {
            var loginData = new { email = email, password = password };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await ApiClient.Http.PostAsync("users/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseJson);

                if (result != null && !string.IsNullOrEmpty(result.Token))
                {
                    // Guardamos el token y el rol en AuthService para consultar desde la UI
                    AuthService.CurrentToken = result.Token;
                    AuthService.CurrentUserRole = result.Role;

                    // Configuramos el token en ApiClient para las peticiones al servidor
                    ApiClient.SetAuthToken(result.Token);
                    
                    return true;
                }
            }

            return false;
        }
    }
}