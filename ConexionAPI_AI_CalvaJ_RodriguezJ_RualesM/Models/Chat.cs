using System.ComponentModel.DataAnnotations;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Provider { get; set; }
        [Required]
        public string UserPrompt { get; set; }
        [Required]
        public string BotResponse { get; set; }
        
        public DateTime CreatedAT { get; set; } = DateTime.Now;
    }
}
