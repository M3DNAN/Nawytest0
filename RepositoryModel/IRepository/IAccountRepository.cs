using Microsoft.AspNetCore.Identity;
using RepositoryModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository
{
    public interface IAccountRepository : IEmailService
    {
        Task<ApplicationUser> FindUserByIdAsync(string userId);
        Task<ApplicationUser> FindUserByEmailAsync(string userId);
        Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password);
        Task<string[]> GenerateToken(ApplicationUser userLog, string status);
        Task ExternalFromOutSide(ApplicationUser user);
        Task NotUserExternal(ApplicationUser user);
        Task<Token> FindRefreshTokenAsync(string refreshToken);
        Task<Token> FindTokenAsync(string Token);
        Task AddToRoleAsync(ApplicationUser user, string role);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser applicationUser, string token, string newpassowrd);

        bool CheckUniqueEmail(string email);
        string CreateUserName(string FirstName, string LastName);
    }
}
