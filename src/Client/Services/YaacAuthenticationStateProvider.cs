using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Yaac.Client.Services;

public class YaacAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    public YaacAuthenticationStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await _localStorage.GetItemAsStringAsync(UIConstants.AuthTokenLocalStorageKey);
        var identity = new ClaimsIdentity();
        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!String.IsNullOrEmpty(authToken))
        {
            try
            {
                identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
            }
            catch (Exception)
            {
                await _localStorage.RemoveItemAsync(UIConstants.AuthTokenLocalStorageKey);
                identity = new ClaimsIdentity();
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(state));
        return state;
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string authToken)
    {
        var payload = authToken.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes)!;
        var claims = keyValuePairs.Select(p => new Claim(p.Key, p.Value.ToString() ?? String.Empty));
        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }
        return Convert.FromBase64String(base64);
    }
}