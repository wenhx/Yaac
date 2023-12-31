﻿using Yaac.Shared;
using Yaac.Shared.Models;

namespace Yaac.Client.Services;

public interface IAuthService
{
    Task<InvokedResult> SignInAsync(SignInModel model);
    Task<InvokedResult> SignOutAsync();
    Task<InvokedResult> SignUpAsync(SignUpModel model);
}