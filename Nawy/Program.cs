
using DataBaseModel;
using DataBaseModel.Repositories;
using Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryModel;
using RepositoryModel.IRepository;
using RepositoryModel.Models;
using System.Text;

namespace Nawy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            //builder.Services.Configure<FormOptions>(options =>
            //{
            //    options.MultipartBodyLengthLimit = 104857600; // 100 MB
            //});
            builder.Services.AddSwaggerGen(options =>
            {
                options.MapType<IFormFile>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                });
            });





            builder.Services.AddCors(options =>
            {
                options.AddPolicy("All", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });


            builder.Services.AddDbContext<ApplicationDbContext>(
                   options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                   ));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IUnitOFWork, UnitOFWork>();

            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // https check
                options.SaveToken = true; // https check
                var secretKeys = builder.Configuration.GetSection("Jwt:SecretKeys").Get<List<string>>();

                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7232/",

                    IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                    {
                        // Create a list of SymmetricSecurityKey objects
                        return secretKeys.Select(secret =>
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)));
                    }
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully!");
                        foreach (var claim in context.Principal.Claims)
                        {
                            Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                        }
                        return Task.CompletedTask;
                    }
                };


            })
          .AddGoogle(options =>
          {
              options.ClientId = "697395799982-je70ld4ulsi1vqci04n46uc53u6lk0sb.apps.googleusercontent.com";
              options.ClientSecret = "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";
              options.SaveTokens = true;
          });

            ;
         
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")),
                RequestPath = "/images"
            });

            app.UseHttpsRedirection();
            app.UseCors("All");
        
            //app.UseMiddleware<TokenValidationMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
