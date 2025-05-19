namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models
{
    public class GroqModel
    {
        public string model { get; set; } = "llama-3.3-70b-versatile";
        public List<GroqMessage> messages { get; set; }
    }

    public class GroqMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
