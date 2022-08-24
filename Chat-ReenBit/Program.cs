using DataBase;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Chat_ReenBit.ChatHub;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Chat_ReenBit.Identity;
using Microsoft.AspNetCore.Identity;
using Chat_ReenBit.Services;


namespace Chat_ReenBit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Azure_chatdata_Connection");
            var identityconnectionString = builder.Configuration.GetConnectionString("Azure_chat_Connection");
            // Add services to the container.
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ChatService>();
            builder.Services.AddScoped<MessageService>();
            builder.Services.AddScoped<MessageHub>();
            builder.Services.Add(ServiceDescriptor.Scoped(typeof(IRepository<>), typeof(ChatRepository<>)));
            builder.Services.AddCors();

            builder.Services.AddDbContext<IdentityContext>(options =>
             options.UseSqlServer(identityconnectionString));
            builder.Services.AddDbContext<Context>(options =>
             options.UseSqlServer(connectionString));

            builder.Services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist/chat-reenbit";
                });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            builder.Services.AddSignalR();
            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.SetIsOriginAllowed(origin=>true)
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));
            builder.Services.AddAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.SameSite = SameSiteMode.None;
            });
            var app = builder.Build();
            app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                spa.UseAngularCliServer(npmScript: "start");
            });
            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHub<MessageHub>("/chatHub");
            app.Run();
        }
    }
}