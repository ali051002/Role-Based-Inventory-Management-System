using IMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Radzen;
using System.Text;
using System.Text.Json;

namespace IMS.Web.Components.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        [SupplyParameterFromForm]
        private LoginRequestDto _LoginModel { get; set; } = new LoginRequestDto();

        private string inValidMessage = string.Empty;

        private bool isLoading = false;

        private bool showPassword = false;

        public string? inputType = "password";

        private void ToggleShowPassword()
        {
            if (showPassword)
            {
                inputType = "password";
                showPassword = false;
            }
            else
            {
                inputType = "text";
                showPassword = true;
            }
        }

        public async Task Submit()
        {
            try
            {
                isLoading = true;
                var request = new HttpRequestMessage(HttpMethod.Post, config.GetValue<string>("ApiUrl") + "Auth/Login");
                request.Content = new StringContent(JsonSerializer.Serialize(_LoginModel), Encoding.UTF8, "application/json");
                var response = await new HttpClient().SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    inValidMessage = string.Empty;
                    var data = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                    await localStorage.SetAsync("AccessToken", data.AccessToken);
                    await localStorage.SetAsync("RefreshToken", data.RefreshToken);
                    await localStorage.SetAsync("UserName", data.UserName);
                    await localStorage.SetAsync("Role", data.Role);

                    await loginState.InitializeAsync();
                    navigationManager.NavigateTo("/home");
                }
                else
                {
                    inValidMessage = "Invalid login attempt. Please check your credentials.";
                }
            }
            catch (Exception ex)
            {
                var message = new NotificationMessage
                {
                    Style = "position: fixed !important; top: 20px; left: 50%; transform: translateX(-50%); z-index: 9999;",
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = "Something went wrong.",
                    Duration = 4000
                };
                notificationService.Notify(message);
                throw new Exception($"Error Logging In : {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

        private void InvalidSubmit()
        {
            Console.WriteLine("Invalid Submit");
        }
    }
}
