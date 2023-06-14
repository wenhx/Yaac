using System.Diagnostics;
using Yaac.Shared;
using Yaac.Shared.Models;

namespace Yaac.Client.Services;

public class AuthService : IAuthService
{
    private readonly IAuthApi _authApi;

    public AuthService(IAuthApi authApi)
    {
        _authApi = authApi;
    }

    public Task<InvokedResult> SignInAsync(SignInModel model)
    {
        return Task.FromResult(InvokedResult.Fail("The method for Sign in has not yet been implemented."));
    }

    public async Task<InvokedResult> SignUpAsync(SignUpModel model)
    {
        var result = await _authApi.SignUpAsync(model);
        Debug.WriteLine($"User [{model.UserName}] registered {result.Succeeded}");
        return result;
    }
}