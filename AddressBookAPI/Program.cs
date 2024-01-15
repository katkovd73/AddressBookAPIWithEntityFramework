using AddressBookAPI.Mappers;
using AddressBookAPI.Models;
using AddressBookAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AddressBookAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);           

            // Add services to the container.
            builder.Services.AddTransient<IContactService, ContactService>();
            builder.Services.AddScoped<MyMapper>();

            // Configuring Entity Framework
            builder.Services.AddDbContext<dbcontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
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