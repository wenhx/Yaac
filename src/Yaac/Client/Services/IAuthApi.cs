using Yaac.Shared;
using Yaac.Shared.Models;

namespace Yaac.Client.Services;

public interface IAuthApi
{
    Task<InvokedResult> SignUpAsync(SignUpModel model);
}
