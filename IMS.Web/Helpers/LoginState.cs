using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace IMS.Web.Helpers
{
    public class LoginState(ProtectedLocalStorage localStorage)
    {
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public async Task InitializeAsync()
        {
            var userName = await localStorage.GetAsync<string>("UserName");
            var role = await localStorage.GetAsync<string>("Role");
            var accessToken = await localStorage.GetAsync<string>("AccessToken");
            var refreshToken = await localStorage.GetAsync<string>("RefreshToken");

            IsLoggedIn = userName.Success ? true : false;
            UserName = userName.Success ? userName.Value : string.Empty;
            Role = role.Success ? role.Value : string.Empty;
            AccessToken = accessToken.Success ? accessToken.Value : string.Empty;
            RefreshToken = refreshToken.Success ? refreshToken.Value : string.Empty;
        }

        public async void SetLogout()
        {
            await localStorage.DeleteAsync("AccessToken");
            await localStorage.DeleteAsync("RefreshToken");
            await localStorage.DeleteAsync("UserName");
            await localStorage.DeleteAsync("Role");

            IsLoggedIn = false;
            UserName = string.Empty;
            AccessToken = string.Empty;
            Role = string.Empty;
        }
    }
}
