﻿using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;
using System.Security.Claims;
using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.Domain.Models.Constants;

namespace SportsHub.AppService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> GetByEmailOrUsernameAsync(string usernameOrEmail)
        {
            return await _unitOfWork.UserRepository.GetByUsernameOrEmailAsync(usernameOrEmail)??
                   throw new NotFoundException(404, ExceptionMessages.NotFound);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _unitOfWork.UserRepository.GetByEmailAsync(email)??
                   throw new NotFoundException(404, ExceptionMessages.NotFound);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _unitOfWork.UserRepository.GetByUsernameAsync(username)??
                   throw new NotFoundException(404, ExceptionMessages.NotFound);
        }

        public async Task<User?> GetUserByClaimsAsync(ClaimsIdentity identity)
        {
            if (identity == null) throw new BusinessLogicException(400, ExceptionMessages.BussinesError);
            

            var userClaims = identity.Claims;
            var username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(username)??
                       throw new NotFoundException(404, ExceptionMessages.NotFound);

            return new User()
            {
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }

        public async Task SaveUserAsync(User user)
        {
            await _unitOfWork.UserRepository.SaveUserAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
