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
    public static class UsersService
    {
        public static async Task<List<User>> GetUsersAsync()
        {
            var json = await ApiClient.Http.GetStringAsync("users/all");
            return JsonConvert.DeserializeObject<List<User>>(json);
        }
    }
}
