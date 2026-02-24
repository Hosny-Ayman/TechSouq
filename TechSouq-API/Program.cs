using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using System.Text.Json.Serialization;
using TechSouq.Application;
using TechSouq.Application.Services;
using TechSouq.Application.Validators;
using TechSouq.DataLayer.Repositories;
using TechSouq.Domain.Entities;
using TechSouq.Domain.Interfaces;
using TechSouq.Domian.Interfaces;
using TechSouq.Infrastructure.Data;
using TechSouq.Infrastructure.Repositories;



namespace TechSouq_API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Serilog.Debugging.SelfLog.Enable(msg => System.Diagnostics.Debug.WriteLine(msg));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithProperty("Application", "TechSouq_API")
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.File(
                @"D:\Programming 2026\TechSouq Project\TechSouqLogs\log-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                buffered: false)
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            Log.Information("Program Work Good");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog();

                var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

                builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {

                        var errors = actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage)
                            .ToList();


                        var logger = actionContext.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                       
                        var path = actionContext.HttpContext.Request.Path;
                        logger.LogWarning("Automatic Validation Failed at Endpoint: {Path}. Errors: {@Errors}", path, errors);

                        var operationResult = OperationResult<object>.BadRequest("Validation Error", errors);

                       
                        return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(operationResult);
                    };
                });

                builder.Services.AddScoped<IAddressRepository, AddressRepository>();
                builder.Services.AddScoped<AddressService>();

                builder.Services.AddScoped<IBrandRepository, BrandRepository>();
                builder.Services.AddScoped<BrandService>();

                builder.Services.AddScoped<ICartRepository, CartRepository>();
                builder.Services.AddScoped<CartService>();

                builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
                builder.Services.AddScoped<CartItemService>();

                // Add services to the container.


                builder.Services.AddFluentValidationAutoValidation();
                //builder.Services.AddFluentValidationClientsideAdapters();

                builder.Services.AddValidatorsFromAssemblyContaining<AddressValidator>();
                builder.Services.AddValidatorsFromAssemblyContaining<brandValidator>();
                builder.Services.AddValidatorsFromAssemblyContaining<CartValidator>();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddAutoMapper(typeof(TechSouq.Application.Mappings.MappingProfiles));

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
           
        }
    }
}
