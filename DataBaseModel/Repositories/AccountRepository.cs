using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryModel.IRepository;
using RepositoryModel.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModel.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;
        public AccountRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext
      , IConfiguration configuration
            , IEmailService emailService
            )
        {

            this.userManager = userManager;
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.emailService = emailService;
        }


        public async Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password)
        {        
            return await userManager.CreateAsync(user, password);
        }
        public async Task NotUserExternal(ApplicationUser user)
        {
            var appusernew = await userManager.FindByEmailAsync(user.Email);
            if (appusernew != null)
            {
                appusernew.External = false;
                dbContext.SaveChanges();
            }
        }
        public async Task ExternalFromOutSide(ApplicationUser user)
        {
            var appusernew = await userManager.FindByEmailAsync(user.Email);
            if (appusernew != null)
            {
                appusernew.External = true;
                dbContext.SaveChanges();
            }
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }
        public async Task<ApplicationUser> FindUserByEmailAsync(string userId)
        {
            return await userManager.FindByEmailAsync(userId);
        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            await userManager.AddToRoleAsync(user, role);
        }

   
        public bool CheckUniqueEmail(string email)
        {
            var ExistingEmail = dbContext.Users.Where(e => e.Email == email).FirstOrDefault();

            if (ExistingEmail != null)
            {
                return false;
            }
            return true;
        }

        public string CreateUserName(string FirstName, string LastName)
        {
            string UserName = FirstName + LastName + Guid.NewGuid().ToString("N");

            return UserName;

        }

        public async Task<string[]> GenerateToken(ApplicationUser userLog, string status)
        {
            List<Claim> signclaims = new List<Claim>() { };
            signclaims.Add(new Claim("Id", userLog.Id));
            signclaims.Add(new Claim("userName", userLog.UserName));
            signclaims.Add(new Claim("national-id", userLog.NationalId ?? "notset"));
            signclaims.Add(new Claim("role", "admin"));
            signclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            List<Claim> signclaimsRefresh = new List<Claim>() { };
            signclaimsRefresh.Add(new Claim("Id", userLog.Id));
            signclaimsRefresh.Add(new Claim("userName", userLog.UserName));
            signclaimsRefresh.Add(new Claim("national-id", userLog.NationalId ?? "notset"));
            signclaimsRefresh.Add(new Claim("role", "admin"));
            signclaimsRefresh.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


            var secretKeys = configuration.GetSection("Jwt:SecretKeys").Get<List<string>>();
            var selectedKey = secretKeys[new Random().Next(secretKeys.Count)]; // Randomly select a key

            //var secretKey = configuration["Jwt:SecretKeys:0"];

            var Cred = new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(selectedKey))
                            , SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: "https://localhost:7232/",
                            //expires: DateTime.Now.AddMinutes(2) // global time
                            expires: DateTime.SpecifyKind(DateTime.Now.AddMinutes(2), DateTimeKind.Local), // Local time
                             audience: "http://localhost:3000/",
                            claims: signclaims,
                            signingCredentials: Cred
            );
            JwtSecurityToken jwtSecurityTokenrefresh = new JwtSecurityToken(
                            issuer: "https://localhost:7232/",
                            audience: "http://localhost:3000/",
                            //expires: DateTime.Now.AddMinutes(2) // global time
                            expires: DateTime.SpecifyKind(DateTime.Now.AddMinutes(2), DateTimeKind.Local) // Local time
                            ,
                            claims: signclaimsRefresh,
                            signingCredentials: Cred
            );
         




            string jti = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            string refreshjti = jwtSecurityTokenrefresh.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken); // serialize to string
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityTokenrefresh); // serialize refresh token
           
            
            JwtSecurityToken parsedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            JwtSecurityToken parsedTokenRefresh = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);


            if (status == "new")
            {
                await dbContext.Tokens.AddAsync(new Token()
                {
                    TokenId = jti,
                    ApplicationUserId = userLog.Id,
                    //RefreshTokenExpiryTime = parsedTokenRefresh.ValidTo ,
                    RefreshTokenExpiryTime=DateTime.Now.AddMinutes(6),
                    TokenExpiryTime = DateTime.Now.AddMinutes(2),
                    TokenEncode = token,
                    TimeOfLog = DateTime.Now,
                    RefreshToken = refreshToken,
                });

            }
            else if (status == "refresh")
            {
                await dbContext.Tokens.AddAsync(new Token()
                {
                    TokenId = refreshjti,
                    ApplicationUserId = userLog.Id,
                    //RefreshTokenExpiryTime = parsedTokenRefresh.ValidTo ,
                    RefreshTokenExpiryTime = DateTime.Now.AddMinutes(2),
                    TokenExpiryTime = DateTime.Now.AddMinutes(6),
                    TokenEncode = refreshToken,
                    TimeOfLog = DateTime.Now,
                    RefreshToken = token,
                });

            }
            else
            {

                List<Claim> signclaimsReset = new List<Claim>() { };
                signclaimsReset.Add(new Claim("Id", userLog.Id));
                signclaimsReset.Add(new Claim("userName", userLog.UserName));
                signclaimsReset.Add(new Claim("national-id", userLog.NationalId ?? "notset"));
                signclaimsReset.Add(new Claim("role", "passwordforget"));
                signclaimsReset.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                JwtSecurityToken jwtSecurityTokenresetPassword = new JwtSecurityToken(
                        issuer: "https://localhost:7232/",
                        audience: "http://localhost:3000/",
                            //expires: DateTime.Now.AddMinutes(2) // global time
                            expires: DateTime.SpecifyKind(DateTime.Now.AddMinutes(2), DateTimeKind.Local), // Local time
                        claims: signclaimsReset,
                        signingCredentials: Cred
        );
                string resetPasswordjti = jwtSecurityTokenresetPassword.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

                string resetToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityTokenresetPassword);

                JwtSecurityToken ParsedresetToken = new JwtSecurityTokenHandler().ReadJwtToken(resetToken);

                await dbContext.Tokens.AddAsync(new Token()
                {
                    TokenId = resetPasswordjti,
                    ApplicationUserId = userLog.Id,
                    // you should limit this time to become less time to avoid attacks conter
                    //TokenExpiryTime = ParsedresetToken.ValidTo,
                    TokenExpiryTime= DateTime.Now.AddMinutes(2),
                    TokenEncode = resetToken,
                    TimeOfLog = DateTime.Now,
                    RefreshToken = "norefresh",
                });
                await dbContext.SaveChangesAsync();

                return new string[]{resetToken ,
                refreshToken
                 };

            }
            await dbContext.SaveChangesAsync();


            return new string[]{token ,
                refreshToken
                 };
        }



        public async Task<Token> FindRefreshTokenAsync(string refreshToken)
        {
            var token = await dbContext.Tokens.Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            return token;
        }

        public async Task<Token> FindTokenAsync(string Token)
        {
            var token = await dbContext.Tokens.Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(u => u.TokenEncode == Token);
            return token;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await emailService.SendEmailAsync(email, subject, message);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser applicationUser, string token, string newpassowrd)
        {

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            applicationUser.PasswordHash = passwordHasher.HashPassword(applicationUser, newpassowrd);
            var updateResult = await userManager.UpdateAsync(applicationUser);
            return updateResult;
        }

    }
}
