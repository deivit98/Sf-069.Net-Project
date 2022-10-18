﻿using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Authentication
{
    public interface IAuthenticationService
    {
        public Task<bool> RegisterUserAsync(UserRegisterDTO userInput);
        public Task<User?> Authenticate(UserLoginDTO userLogin);
    }
}
