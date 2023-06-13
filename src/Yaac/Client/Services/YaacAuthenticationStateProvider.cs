using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Yaac.Client.Services;

public class YaacAuthenticationStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal();
        var state = new AuthenticationState(user);
        return Task.FromResult(state);
    }
}
