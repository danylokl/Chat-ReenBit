using DataBase;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Test_Task_ReenBit.Services;
using Microsoft.AspNetCore.SpaServices.AngularCli;
namespace Chat_ReenBit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // Add services to the container.
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ChatService>();
            builder.Services.AddScoped<MessageService>();
            builder.Services.Add(ServiceDescriptor.Scoped(typeof(IRepository<>), typeof(ChatRepository<>)));
            builder.Services.AddDbContext<Context>(options =>
             options.UseSqlServer(connectionString));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}