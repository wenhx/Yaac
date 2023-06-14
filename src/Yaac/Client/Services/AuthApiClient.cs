using System.Net.Http.Json;
using Yaac.Shared;
using Yaac.Shared.Models;

namespace Yaac.Client.Services;

public class AuthApiClient : IAuthApi
{
    private readonly HttpClient _httpClient;

    public AuthApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<InvokedResult> SignUpAsync(SignUpModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/signup", model);
        var result = await response.Content.ReadFromJsonAsync<InvokedResult>();
        Utility.EnsureResultNotNull(result);
        return result!;
    }
}