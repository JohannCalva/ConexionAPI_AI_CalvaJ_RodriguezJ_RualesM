using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models;
using Microsoft.EntityFrameworkCore;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Data
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options)
            : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional de la tabla Chat
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Provider).IsRequired().HasMaxLength(50);

                entity.Property(c => c.UserPrompt).IsRequired();

                entity.Property(c => c.BotResponse).IsRequired();

                entity.Property(c => c.CreatedAT).IsRequired();
            });
        }
    }
}