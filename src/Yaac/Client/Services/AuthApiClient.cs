using System.Diagnostics;
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

    public async Task<InvokedResult<string>> SignInAsync(SignInModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/signin", model);
        await EnsureNotInternalServerError(response);
        var result = await response.Content.ReadFromJsonAsync<InvokedResult<string>>();
        Utility.EnsureResultNotNull(result);
        return result!;
    }

    public async Task<InvokedResult> SignUpAsync(SignUpModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/signup", model);
        await EnsureNotInternalServerError(response);
        var result = await response.Content.ReadFromJsonAsync<InvokedResult>();
        Utility.EnsureResultNotNull(result);
        return result!;
    }

    private async Task EnsureNotInternalServerError(HttpResponseMessage response)
    {
        if ((int)response.StatusCode < 500)
            return;

        var error = await response.Content.ReadAsStringAsync();
        Utility.ConsoleDebug(error);
        throw new Exception("Internal server error");
    }
}