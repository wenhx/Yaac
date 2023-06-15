using Yaac.Shared;
using Yaac.Shared.Models;

namespace Yaac.Client.Services;

public interface IAuthApi
{
    Task<InvokedResult<string>> SignInAsync(SignInModel model);
    Task<InvokedResult> SignUpAsync(SignUpModel model);
}
