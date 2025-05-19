using System.Text;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Interfaces;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models;
using Newtonsoft.Json;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Repositories
{
    public class GroqRepository : IChatbotService
    {
        private readonly HttpClient _httpClient;

        private readonly string GroqKey = "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";
        public GroqRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GroqKey);

        }

        public async Task<string> GetChatbotResponseAsync(string prompt)
        {
            string url = "https://api.groq.com/openai/v1/chat/completions";

            var request = new GroqModel
            {
                model = "llama-3.3-70b-versatile",
                messages = new List<GroqMessage>
                {
                    new GroqMessage { role = "user", content = prompt }
                }
            };

            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = content;

            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();

            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
            string resultText = jsonResponse?.choices?[0]?.message?.content;

            return resultText ?? "No response from model.";
        }
    }
}
