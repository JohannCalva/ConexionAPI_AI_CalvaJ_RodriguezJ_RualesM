using System.Text;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Interfaces;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models;
using Newtonsoft.Json;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Repositories
{
    public class GeminiRepository : IChatbotService
    {
        private HttpClient _httpClient;
        private string geminiApiKey = "AIzaSyBZFA1dpLS1DejGK4otzvJCCmf1dgSn6SI";
        public GeminiRepository()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetChatbotResponseAsync(string prompt)
        {
            string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + geminiApiKey;
            GeminiRequest request = new GeminiRequest
            {
                contents = new List<GeminiContent>
                {
                    new GeminiContent
                    {
                        parts = new List<GeminiPart>
                        {
                            new GeminiPart
                            {
                                text = prompt
                            }
                        }
                    }
                }
            };
            string requestJson = JsonConvert.SerializeObject(request); // Serializar el objeto a JSON
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error en la solicitud: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");

            var answer = await response.Content.ReadFromJsonAsync<GeminiResponse>();

            var responseText = answer.candidates
                        .SelectMany(candidate => candidate.content.parts)
                        .Select(part => part.text)
                        .FirstOrDefault() ?? "No se pudo obtener respuesta del chatbot.";

            return responseText;
        }

        public Task<bool> SaveResponseInDatabase(string chatbotPrompt, string chatBotResponse)
        {
            throw new NotImplementedException();
        }
    }
}
