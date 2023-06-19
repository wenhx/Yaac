using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Principal;

namespace Yaac.Client;

public class YaacComponentBase : ComponentBase
{
    [CascadingParameter]
    Task<AuthenticationState>? AuthenticationStateTask { get; set; }

    public async Task<IIdentity?> GetCurrentUser()
    {
        if (AuthenticationStateTask == null)
            return null;

        var authState = await AuthenticationStateTask;
        var user = authState.User;
        return user.Identity;
    }
}