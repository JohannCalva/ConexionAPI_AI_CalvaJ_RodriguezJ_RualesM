namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models
{
    public class GeminiRequest
    {
        public List<GeminiContent> contents { get; set; }
    }
    public class GeminiContent
    {
        public List<GeminiPart> parts { get; set; }
    }
    public class GeminiPart
    {
        public string text { get; set; }
    }
    public class GeminiResponse
    {
        public List<GeminiCandidate> candidates { get; set; }
    }
    public class GeminiCandidate
    {
        public GeminiContent content { get; set; }
    }
}
