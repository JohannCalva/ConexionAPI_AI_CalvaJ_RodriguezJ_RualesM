namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Interfaces
{
    public interface IChatbotService
    {
        public Task<string> GetChatbotResponseAsync(string prompt);
    }
}
