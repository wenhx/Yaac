using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using Yaac.Shared;
using Yaac.Shared.Models;

namespace Yaac.Client.Services;

public class AuthService : IAuthService
{
    private readonly IAuthApi _authApiClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthService(IAuthApi authApi, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
    {
        _authApiClient = authApi;
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<InvokedResult> SignInAsync(SignInModel model)
    {
        var result = await _authApiClient.SignInAsync(model);
        Utility.ConsoleDebug($"User [{model.UserName}] signed in {result.Succeeded}");
        if (result.Succeeded)
        {
            await _localStorage.SetItemAsStringAsync(UIConstants.AuthTokenLocalStorageKey, result.Data);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
        }
        return result;
    }

    public async Task<InvokedResult> SignOutAsync()
    {
        Utility.ConsoleDebug("Removing auth token.");
        await _localStorage.RemoveItemAsync(UIConstants.AuthTokenLocalStorageKey);
        await _authenticationStateProvider.GetAuthenticationStateAsync();
        return InvokedResult.Success;
    }

    public async Task<InvokedResult> SignUpAsync(SignUpModel model)
    {
        var result = await _authApiClient.SignUpAsync(model);
        Utility.ConsoleDebug($"User [{model.UserName}] signed up {result.Succeeded}");
        return result;
    }
}