using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models;
using Microsoft.EntityFrameworkCore;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Data
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuración adicional de la tabla Chat
            modelBuilder.Entity<Chat>()
                .ToTable("Chats")
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<Chat>()
                .Property(c => c.Provider)
                .IsRequired()
                .HasMaxLength(50);
                
            modelBuilder.Entity<Chat>()
                .Property(c => c.UserPrompt)
                .IsRequired();
                
            modelBuilder.Entity<Chat>()
                .Property(c => c.BotResponse)
                .IsRequired();
                
            modelBuilder.Entity<Chat>()
                .Property(c => c.CreatedAt)
                .IsRequired();
        }
    }
}