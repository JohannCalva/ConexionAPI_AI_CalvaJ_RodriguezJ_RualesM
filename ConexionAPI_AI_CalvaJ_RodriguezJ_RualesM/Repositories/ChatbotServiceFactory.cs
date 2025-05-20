using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Interfaces;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Repositories
{
    public class ChatbotServiceFactory : IChatbotServiceFactory
    {
        private readonly GroqRepository _GroqRepository;
        private readonly GeminiRepository _GeminiRepository;
        public ChatbotServiceFactory(GroqRepository groqRepository, GeminiRepository geminiRepository)
        {
            _GroqRepository = groqRepository;
            _GeminiRepository = geminiRepository;
        }
        public IChatbotService GetService(string provider)
        {
            return provider switch
            {
                "Gemini" => new GeminiRepository(),
                "Groq" => new GroqRepository(),
                _ => throw new ArgumentException("Invalid provider", nameof(provider))
            };
        }
    }
}
