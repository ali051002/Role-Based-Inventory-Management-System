using IMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace IMS.Web.Services
{
    public class ClientApiService
    {
        #region variables declaration
        private readonly IConfiguration _config;
        private readonly ProtectedLocalStorage _localStorage;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NavigationManager _navigationManager;
        #endregion

        #region Constructor

        public ClientApiService(IConfiguration config, ProtectedLocalStorage localStorage, IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
        {
            _config = config;
            _localStorage = localStorage;
            _httpClientFactory = httpClientFactory;
            _navigationManager = navigationManager;
        }
        #endregion

        #region Get Client
        private HttpClient GetClient(string clientName)
        {
            return _httpClientFactory.CreateClient(clientName);
        }
        #endregion

        #region Send POST with Body
        public async Task<ApiResponse> SendAsync<TRequest, TResponse>(HttpMethod method, string endpoint, TRequest requestData, bool hasReturnData = true)
        {
            try
            {
                var client = GetClient("IMS");

                var request = new HttpRequestMessage(method, endpoint)
                {
                    Content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json")
                };

                var token = await _localStorage.GetAsync<string>("AccessToken");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    object resultData = null;
                    if (hasReturnData)
                    {
                        resultData = await response.Content.ReadFromJsonAsync<TResponse>();
                    }

                    return new ApiResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = true, ResultData = resultData };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var newToken = await RefreshTokenAsync();

                    if (string.IsNullOrEmpty(newToken))
                    {
                        _navigationManager.NavigateTo("/unauthorized");
                        return new ApiResponse { StatusCode = HttpStatusCode.Unauthorized, Message = "ApiClientService: Invalid Refresh Token", IsSuccessStatusCode = false };
                    }

                    var refreshedRequest = new HttpRequestMessage(method, endpoint)
                    {
                        Content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json")
                    };

                    refreshedRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

                    var refreshedTokenResponse = await client.SendAsync(refreshedRequest);

                    if (refreshedTokenResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return new ApiResponse { StatusCode = refreshedTokenResponse.StatusCode, IsSuccessStatusCode = false };
                    }

                    object resultData = null;

                    if (hasReturnData)
                    {
                        resultData = await refreshedTokenResponse.Content.ReadFromJsonAsync<TResponse>();
                    }

                    return new ApiResponse { StatusCode = refreshedTokenResponse.StatusCode, IsSuccessStatusCode = true, ResultData = resultData };
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<BadRequestResponse>();
                    return new ApiResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = false, ResultData = error };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccessStatusCode = false, ResultData = new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message } };
            }
        }
        #endregion

        #region Send GET with Params
        public async Task<ApiResponse> SendGetAsync<TResponse>(string endpointWithQuery, bool hasReturnData = true)
        {
            try
            {
                var client = GetClient("IMS");
                var request = new HttpRequestMessage(HttpMethod.Get, endpointWithQuery);

                var token = await _localStorage.GetAsync<string>("AccessToken");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    object resultData = null;
                    if (hasReturnData)
                    {
                        resultData = await response.Content.ReadFromJsonAsync<TResponse>();
                    }

                    return new ApiResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = true, ResultData = resultData };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var newToken = await RefreshTokenAsync();

                    if (string.IsNullOrEmpty(newToken))
                    {
                        _navigationManager.NavigateTo("/unauthorized");
                        return new ApiResponse { StatusCode = HttpStatusCode.Unauthorized, Message = "ApiClientService: Invalid Refresh Token", IsSuccessStatusCode = false };
                    }

                    var refreshedRequest = new HttpRequestMessage(HttpMethod.Get, endpointWithQuery);

                    refreshedRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

                    var refreshedTokenResponse = await client.SendAsync(refreshedRequest);

                    if (refreshedTokenResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return new ApiResponse { StatusCode = refreshedTokenResponse.StatusCode, IsSuccessStatusCode = false };
                    }

                    object resultData = null;

                    if (hasReturnData)
                    {
                        resultData = await refreshedTokenResponse.Content.ReadFromJsonAsync<TResponse>();
                    }

                    return new ApiResponse { StatusCode = refreshedTokenResponse.StatusCode, IsSuccessStatusCode = true, ResultData = resultData };
                }
                else
                {
                    var error = await response.Content.ReadFromJsonAsync<BadRequestResponse>();
                    return new ApiResponse { StatusCode = response.StatusCode, IsSuccessStatusCode = false, ResultData = error };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccessStatusCode = false, ResultData = new BadRequestResponse { Message = ex.Message, InnerException = ex.InnerException?.Message } };
            }
        }
        #endregion

        #region RefreshToken
        private async Task<string> RefreshTokenAsync()
        {

            var refreshToken = await _localStorage.GetAsync<string>("RefreshToken");

            if (!string.IsNullOrEmpty(refreshToken.Value))
            {
                var url = _config.GetValue<string>("ApiUrl") + "Auth/refreshToken";
                var payload = new { RefreshToken = refreshToken.Value };
                var response = await new HttpClient().PostAsJsonAsync(url, payload);
                var respText = await response.Content.ReadAsStringAsync();
                //Console.WriteLine("Refresh Token Response: " + respText);
                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
                    if (tokenResponse != null)
                    {
                        try
                        {
                            await _localStorage.SetAsync("AccessToken", tokenResponse.Token);
                            await _localStorage.SetAsync("RefreshToken", tokenResponse.RefreshToken);
                        }
                        catch (InvalidOperationException)
                        {
                            return null;
                        }
                        return tokenResponse.Token;
                    }
                    return null;
                }

                return null;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
