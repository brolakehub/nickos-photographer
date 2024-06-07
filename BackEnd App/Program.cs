using BackEnd_App.Data;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_App
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

            builder.Services.AddDbContext<DatabaseContext>(option =>
            {
                option.UseSqlServer($"Server={Environment.GetEnvironmentVariable("dbHost")},{Environment.GetEnvironmentVariable("dbPort")};Database={Environment.GetEnvironmentVariable("dbName")};User Id={Environment.GetEnvironmentVariable("dbUser")};Password={Environment.GetEnvironmentVariable("dbPassword")};");
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AlowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            var app = builder.Build();


            app.UseSwagger();
            app.MapSwagger().AllowAnonymous().RequireCors("AllowAll");

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
