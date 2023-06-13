using Yaac.Shared;
using Yaac.Shared.Models;

namespace Yaac.Client.Services;

public class AuthService : IAuthService
{
    public Task<InvokedResult> SignInAsync(SignInModel model)
    {
        return Task.FromResult(InvokedResult.Fail("The method for Sign in has not yet been implemented."));
    }
}
