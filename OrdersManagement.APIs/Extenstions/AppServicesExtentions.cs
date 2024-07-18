using Microsoft.AspNetCore.Mvc;
using OrdersManagement.APIs.Helpers;
using OrdersManagement.Core.Repositories.Interfaces;
using OrdersManagement.Core;
using OrdersManagement.Repository.Repositories;
using OrdersManagement.Repository;
using OrdersManagement.Service.Invoices;
using OrdersManagement.Service.Product.Contract;
using OrdersManagement.Service.Product;
using OrdersManagement.APIs.Errors;

public static class AppServicesExtensions
{
    public static IServiceCollection AddAppLicationsServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //services.AddScoped<IEmailService, EmailService>(); 

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IValidateTheOrderServices, ValidateTheOrderServices>();
        services.AddScoped<IInvoiceService, InvoiceService>();

        services.AddAutoMapper(typeof(MappingProfile));

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                     .SelectMany(P => P.Value.Errors)
                                                     .Select(E => E.ErrorMessage).ToArray();

                var validationErrorResponse = new ApiValidationErrorResponse()
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(validationErrorResponse);
            };
        });

        return services;
    }
}
