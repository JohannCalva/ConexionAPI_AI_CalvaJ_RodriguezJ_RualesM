using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Data;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Interfaces;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<SQLServerContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("SQLServerContextJJM")));
            
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<GeminiRepository>();
            builder.Services.AddScoped<GroqRepository>();
            builder.Services.AddScoped<ChatRepository>(); // Registrar el repositorio de Chat
            builder.Services.AddScoped<IChatbotServiceFactory, ChatbotServiceFactory>();

            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<SQLServerContext>();
                    dbContext.Database.Migrate();
                }
            }

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
