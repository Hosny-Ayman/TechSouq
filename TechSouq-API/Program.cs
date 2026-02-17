using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechSouq.Application;
using TechSouq.Application.Services;
using TechSouq.Application.Validators;
using TechSouq.Domain.Entities;
using TechSouq.Domain.Interfaces;
using TechSouq.Infrastructure.Data;
using TechSouq.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using TechSouq.Domian.Interfaces;
using TechSouq.DataLayer.Repositories;
using System.Text.Json.Serialization;



namespace TechSouq_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            builder.Services.AddControllers().AddJsonOptions(opetions =>
            opetions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<AddressService>();

            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<BrandServices>();

            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<CartServices>();

            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
            builder.Services.AddScoped<CartItemService>();

            // Add services to the container.

            builder.Services.AddControllers();

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
    }
}
