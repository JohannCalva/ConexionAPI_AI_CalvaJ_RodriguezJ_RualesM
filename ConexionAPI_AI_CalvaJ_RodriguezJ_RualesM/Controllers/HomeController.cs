using System;
using System.Diagnostics;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Interfaces;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Controllers
{
    public class HomeController : Controller
    {
        private IChatbotService _chatbotService;
        public static List<Chat> chatHistory = new List<Chat>();
        public HomeController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        public IActionResult Index()
        {
            ViewBag.ChatHistory = chatHistory;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendPrompt(string provider, string promptUser)
        {
            if (string.IsNullOrEmpty(promptUser)) return RedirectToAction("Index");

            string response = await _chatbotService.GetChatbotResponseAsync(promptUser);
            string htmlResponse = Markdown.ToHtml(response);

            chatHistory.Add(new Chat { Provider = provider, UserPrompt = promptUser, BotResponse = htmlResponse });

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
