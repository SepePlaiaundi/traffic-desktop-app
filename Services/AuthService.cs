using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public static string CurrentUserRole { get; private set; }
        public static string CurrentToken { get; private set; }

        public AuthService()
        {
            _httpClient = new HttpClient { BaseAddress = new System.Uri("http://localhost:8080") };
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var loginRequest = new { email = email, password = password };

            var response = await _httpClient.PostAsJsonAsync("/users/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                CurrentToken = result.Token;
                CurrentUserRole = result.Role;

                return true;
            }

            return false;
        }

        public static bool IsAdmin()
        {
            return CurrentUserRole == "ADMIN";
        }

        public static void Logout()
        {
            CurrentToken = null;
            CurrentUserRole = null;
        }
    }
}