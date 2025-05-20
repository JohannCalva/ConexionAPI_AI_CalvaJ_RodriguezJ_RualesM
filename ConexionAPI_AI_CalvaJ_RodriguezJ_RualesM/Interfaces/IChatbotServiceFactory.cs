namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Interfaces
{
    public interface IChatbotServiceFactory
    {
        IChatbotService GetService(string provider);
    }
}
