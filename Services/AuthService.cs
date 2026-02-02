namespace TrafficDesktopApp.Services
{
    /// <summary>
    /// Esta clase guarda la información de la sesión actual: el token y el rol del usuario.
    /// Es estática para que sea fácil acceder desde cualquier parte de la aplicación.
    /// </summary>
    public static class AuthService
    {
        public static string CurrentToken { get; set; }
        public static string CurrentUserRole { get; set; }

        public static bool IsAdmin()
        {
            return CurrentUserRole == "ADMIN";
        }

        public static void Logout()
        {
            CurrentToken = null;
            CurrentUserRole = null;
            ApiClient.SetAuthToken(null);
            UsersService.ClearCache();
        }
    }
}