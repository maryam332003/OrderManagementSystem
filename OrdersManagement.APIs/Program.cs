using Microsoft.EntityFrameworkCore;
using OrdersManagement.Repository.Identity;
using OrdersManagement.APIs.Extenstions;
using OrdersManagement.APIs.MiddleWares;
using OrdersManagement.Repository.Data;
using Microsoft.AspNetCore.Identity;
using OrdersManagement.Core.Entities;
using Microsoft.OpenApi.Models;
using OrdersManagement.APIs.Helpers;
using OrdersManagement.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
namespace OrdersManagement.APIs
{

    public class Program
	{
        public static async Task Main(string[] args)
		{


			var webApplicationbuilder = WebApplication.CreateBuilder(args);

			// Add services to the container.


			webApplicationbuilder.Services.AddControllers(); 
			webApplicationbuilder.Services.AddEndpointsApiExplorer();
			webApplicationbuilder.Services.AddSwaggerGen();
            webApplicationbuilder.Services.AddSignalR();

            webApplicationbuilder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            webApplicationbuilder.Services.AddDbContext<OrdersDbContext>(options =>
			{

				options.UseSqlServer(webApplicationbuilder.Configuration.GetConnectionString("DefaultConnection"));

			});

			webApplicationbuilder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(webApplicationbuilder.Configuration.GetConnectionString(name: "IdentityConnection"));
			});

			// Identity Services
			webApplicationbuilder.Services.AddIdentityServices(webApplicationbuilder.Configuration);

			// App Services
			webApplicationbuilder.Services.AddAppLicationsServices();
            webApplicationbuilder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Management System API", Version = "v1", Description = "API documentation for the Orders Management system" });
                option.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    }
                );
                option.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
                    }
                );
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });






            var app = webApplicationbuilder.Build();

 

            var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _context = services.GetRequiredService<OrdersDbContext>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{

				await _context.Database.MigrateAsync(); 

				var _userManger = services.GetRequiredService<UserManager<User>>();
			}
			catch (Exception ex)
			{

				var logger = loggerFactory.CreateLogger<Program>();

				logger.LogError(ex, "An Error han been occured during appling the Migrations");


			}
			
            app.UseMiddleware<ExceptionMiddleware>();


			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger(); 

				app.UseSwaggerUI();
			}
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders Management API v1");
                c.RoutePrefix = string.Empty; 


            });
            app.UseStatusCodePagesWithReExecute("/errors/{0}");  

			app.UseHttpsRedirection();
            app.UseRouting(); 
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });
            await app.RunAsync();





        }
	}

}
