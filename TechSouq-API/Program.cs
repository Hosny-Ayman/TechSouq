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



namespace TechSouq_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            builder.Services.AddScoped<IAddressRepository, AddressRepository>();

            builder.Services.AddScoped<AddressService>();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddFluentValidationAutoValidation();
            //builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<AddressValidator>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
