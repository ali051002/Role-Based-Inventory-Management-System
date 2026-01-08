using IMS.Shared.DTOs;
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
        #endregion

        #region Constructor

        public ClientApiService(IConfiguration config, ProtectedLocalStorage localStorage, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _localStorage = localStorage;
            _httpClientFactory = httpClientFactory;
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

                //var token = await _localStorage.GetAsync<string>("Token");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

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

                //var token = await _localStorage.GetAsync<string>("Token");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);


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
    }
}
