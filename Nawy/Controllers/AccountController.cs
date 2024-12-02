using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Nawy.Dtos;
using RepositoryModel;
using RepositoryModel.Models;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Nawy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IUnitOFWork unitOFWork;

        public AccountController(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
        }

        // test to get details of user info
        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var userId = User.FindFirstValue("Id");
            var name = User.FindFirstValue("userName");
            return Ok(new
            {
                id = userId,
                username = name
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> register(Registeruser newaccout)
        {
            if (ModelState.IsValid)
            {
                var checkemail = unitOFWork.Accounts.CheckUniqueEmail(newaccout.Email);
                if (checkemail)
                {
                    ApplicationUser applicationUser = new ApplicationUser()
                    {
                        UserName = unitOFWork.Accounts.CreateUserName(newaccout.FirstName, newaccout.LastName),
                        Email = newaccout.Email
                    };

                    var result = await unitOFWork.Accounts.RegisterUserAsync(applicationUser, newaccout.Password);
                    if (result.Succeeded)
                    {
                        
                        await unitOFWork.Accounts.AddToRoleAsync(applicationUser, "admin");
                        await unitOFWork.Accounts.NotUserExternal(applicationUser);

                        return Ok(new { response = new List<string> { "regstrion success" } });
                    }

                    else
                    {
                        List<string> errorslist = new List<string>();
                        foreach (var error in result.Errors)
                        { errorslist.Add(error.Description); }
                        return BadRequest(new { errors = errorslist });
                    }
                }
                else
                {
                    return BadRequest(new { errors = new List<string> { "There is already an account with this email." } });
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> login(AccountUser newaccout)
        {
            List<string> errors = new List<string>();
            if (ModelState.IsValid)
            {
                ApplicationUser user = await unitOFWork.Accounts.FindUserByEmailAsync(newaccout.Email);
                if (user != null)
                {
                    var result = await unitOFWork.Accounts.CheckPasswordAsync(user, newaccout.Password);
                    if (result)
                    {
                        string[] arraytoken = await unitOFWork.Accounts.GenerateToken(user, "new");
                        return Ok(new
                        {
                            token = arraytoken[0],
                            expiration = DateTime.Now.AddMinutes(2),
                            //should implement that
                            refreshToken = arraytoken[1]
                        });
                    }
                    else
                    {
                        return BadRequest(new { errors = new List<string> { "Wrong Password" } });
                    }

                }
                else
                {
                    return BadRequest(new { errors = new List<string> { "there is no account with this email" } });
                }

            }
            else
            {
                return BadRequest(new { errors = new List<string> { "invalid information" } });
            }
        }



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestinpage request)
        {
            Console.WriteLine(request.refreshToken);
            if(ModelState.IsValid)
            {
                var token = await unitOFWork.Accounts.FindRefreshTokenAsync(request.refreshToken);

                Console.WriteLine($"Request refreshToken: {request.refreshToken}");
                Console.WriteLine($"Token from DB: {token?.RefreshToken}");
                Console.WriteLine($"ApplicationUserId from DB Token: {token?.ApplicationUserId}");
              

                var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
                Console.WriteLine($"Claims: {string.Join(", ", claims.Select(c => $"{c.Type}: {c.Value}"))}");

                var userId = User.FindFirstValue("Id");
                Console.WriteLine($"User ID from Claims: {userId}");
                if (token == null)
                {
                    Console.WriteLine("error in token from token=null");
                    return BadRequest(new { errors = new List<string> { "Invalid or expired session, please login first" } });
                }
                if (token.RefreshTokenExpiryTime > DateTime.Now)
                {
                    Console.WriteLine("error in token from token.ApplicationUserId != userId");
                    return BadRequest(new { errors = new List<string> { "Invalid or expired session, please login first" } });
                }
                if (token.RefreshToken == "norefresh")
                {
                    Console.WriteLine("error in token from token.RefreshToken == norefresh");
                    return BadRequest(new { errors = new List<string> { "Invalid or expired session, please login first" } });
                }
               
                var user = await unitOFWork.Accounts.FindUserByIdAsync(token.ApplicationUserId);
                if (user == null)
                {
                    Console.WriteLine("error in token from no user");
                    return BadRequest(new { errors = new List<string> { "Invalid user" } });
                }

                var newJwtToken = await unitOFWork.Accounts.GenerateToken(user, "refresh");

                unitOFWork.Compelet();
                return Ok(new
                {
                    token = newJwtToken[1],
                    expiration = DateTime.Now.AddMinutes(2),
                    refreshToken = newJwtToken[0]
                });
            }
            else
            {
              
                return BadRequest(ModelState);
            }
           
        }


        [HttpPost("signin-google")]
        public async Task<IActionResult> SignInWithGoogle([FromBody] GoogleSignInRequest googleSignInRequest)
        { if(ModelState.IsValid)
            { 
            try
            {
                var token = googleSignInRequest.token;

                // Validate the token with Google
                var payload = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings
                {
                    // client-id to check
                    Audience = new[] { "697395799982-je70ld4ulsi1vqci04n46uc53u6lk0sb.apps.googleusercontent.com" }
                });

                if (payload == null)
                {
                    Console.WriteLine("Invalid Google token.");
                    return Unauthorized(new { errors = new List<string> { "Invalid Google token." } });
                }

                // Check if the user exists
                var existingUser = await unitOFWork.Accounts.FindUserByEmailAsync(payload.Email);
                if (existingUser == null)
                {
                    Console.WriteLine(payload.Email);
                    string name = unitOFWork.Accounts.CreateUserName(payload.Name, "fromgoogle");
                    string finalname = Regex.Replace(name, @"\s+", "");
                    ApplicationUser newUser = new ApplicationUser
                    {
                        UserName = finalname,
                        Email = payload.Email
                    };

                    var result = await unitOFWork.Accounts.RegisterUserAsync(newUser, "DefaultPassword123!");
                        await unitOFWork.Accounts.ExternalFromOutSide(newUser);

                        if (!result.Succeeded)
                    {
                        Console.WriteLine("Error creating user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                        return BadRequest(new { errors = result.Errors.Select(e => e.Description).ToList() });
                    }


                    existingUser = newUser;
                }

                // Generate JWT Token
                var tokenResponse = await unitOFWork.Accounts.GenerateToken(existingUser, "new");

                return Ok(new
                {
                    token = tokenResponse[0],
                    expiration = DateTime.Now.AddMinutes(2),
                    refreshToken = tokenResponse[1],
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during Google sign-in: " + ex.Message);
                return StatusCode(500, new { error = "An error occurred during Google sign-in." });
            }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgetPasswordDto model)
        {
            if(ModelState.IsValid)
            {
                var user = await unitOFWork.Accounts.FindUserByEmailAsync(model.Email);
                if (user == null)
                {
                    return BadRequest(new { errors = new List<string> { "There is no account associated with this email." } });
                }

                var Tokens = await unitOFWork.Accounts.GenerateToken(user, "reset");
                var resetToken = Tokens[0];
                var refreshToken = Tokens[1];
                var resetUrl = $"http://localhost:3000/reset-password?email={Uri.EscapeDataString(model.Email)}&token={Uri.EscapeDataString(resetToken)}";

                // Send the reset email
                string htmlBody = $@"
<html>
    <body style='background: url(https://media.licdn.com/dms/image/v2/C4D1BAQHdf-CHWbruiw/company-background_10000/company-background_10000/0/1649861173201/nawyestate_cover?e=2147483647&v=beta&t=9dXBWVXjZVbmcO3-w4GL1xIdr4Wdr-1fHxKuGVCNQc0) no-repeat center center; background-size: cover;height:400px;width:400px; font-family: Arial, sans-serif; color: #333; padding: 20px;'>
        <div style='background-color: rgba(255, 255, 255, 0.9); padding: 20px; border-radius: 8px; max-width: 500px; margin: 0 auto; text-align: center;'>
            <h1 style='color: #4CAF50;'>Password Reset</h1>
            <p style='font-size: 16px; line-height: 1.5;'>
                Hello, <br />
                You recently requested to reset your password. Click the button below to reset it:
            </p>
            <a href='{resetUrl}' style='display: inline-block; margin-top: 20px; padding: 10px 20px; color: #fff; background-color: #4CAF50; text-decoration: none; border-radius: 5px; font-size: 16px;'>Reset Password</a>
            <p style='margin-top: 20px; font-size: 14px; color: #777;'>
                If you did not request this, please ignore this email. Your password will remain unchanged.
            </p>
        </div>
    </body>
</html>";

                await unitOFWork.Accounts.SendEmailAsync(user.Email, "Password Reset Request", htmlBody);

                return Ok(new { message = "A password reset link has been sent to your email." });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if(ModelState.IsValid)
            { 
            var user = await unitOFWork.Accounts.FindUserByEmailAsync(model.Email);
                
            if (user == null)
            {
                return BadRequest(new { errors = new List<string> { "Invalid email." } });
            }
        if (user.External == true)
            {
                    return BadRequest(new { errors = new List<string> { "Process is not valid for this email." } });
            }
            // you should check password also
            var tokenrest = await unitOFWork.Accounts.FindTokenAsync(model.Token);
            if (tokenrest == null)
            {
                Console.WriteLine("error is come from token is not found");
                return BadRequest(new { errors = new List<string> { "Invalid process." } });
            }

            //// test cases 
 
            if (tokenrest.RefreshTokenExpiryTime < DateTime.Now)

            {
                Console.WriteLine("error is come from DateTime.Now ");
                return BadRequest(new { errors = new List<string> { "Invalid process." } });
            }
            

           if (tokenrest.RefreshToken == "resetdone"&& (tokenrest.RefreshToken != "norefresh"|| tokenrest.RefreshTokenExpiryTime < DateTime.Now))
            {
                    return BadRequest(new { errors = new List<string> { "Invalid process." } });
            }

           if (tokenrest.RefreshToken != "norefresh")
            {
                    return BadRequest(new { errors = new List<string> { "Invalid process." } });
            }

                var result = await unitOFWork.Accounts.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                Console.WriteLine("error is come from here method hash");
                return BadRequest(new { errors = result.Errors.Select(e => e.Description).ToList() });
            }
                tokenrest.RefreshToken = "resetdone";
                unitOFWork.Compelet();
            return Ok(new { message = "Your password has been reset successfully." });
            }
            else
            {
                return BadRequest(new { errors = new List<string> { "Invalid process." } });
            }
        }

    }
}
