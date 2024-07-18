using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OrdersManagement.Repository.Identity;
using OrdersManagement.Service;
using System.Text;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Repositories.Interfaces;

namespace OrdersManagement.APIs.Extenstions
{
    public static class IdentityServicesExtension
	{


		public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration configuration)
		{
			Services.AddScoped<ITokenService, TokenService>();
			Services.AddIdentity<User, IdentityRole>()
				    .AddEntityFrameworkStores<AppIdentityDbContext>()
					.AddDefaultTokenProviders();
			Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["JWT:ValidIssuer"],

						ValidateAudience = true,
						ValidAudience = configuration["JWT:ValidAudience"],

						ValidateLifetime = true,

						ValidateIssuerSigningKey = true,

						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

					};
				});

			return Services;

		}


	}
}
