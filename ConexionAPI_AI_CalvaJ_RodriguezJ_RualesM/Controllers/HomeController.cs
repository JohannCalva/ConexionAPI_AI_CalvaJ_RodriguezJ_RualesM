using System;
using System.Diagnostics;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Interfaces;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Repositories;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatbotServiceFactory _chatbotServiceFactory;
        private readonly ChatRepository _chatRepository;

        public HomeController(IChatbotServiceFactory chatbotService, ChatRepository chatRepository)
        {
            _chatbotServiceFactory = chatbotService;
            _chatRepository = chatRepository;
        }

        public async Task<IActionResult> Index()
        {
            
            var chatHistory = await _chatRepository.GetAllChatsAsync();
            ViewBag.ChatHistory = chatHistory;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendPrompt(string provider, string promptUser)
        {
            if (string.IsNullOrEmpty(promptUser)) return RedirectToAction("Index");

            var chatbotService = _chatbotServiceFactory.GetService(provider);

            string response = await chatbotService.GetChatbotResponseAsync(promptUser); 
            string htmlResponse = Markdown.ToHtml(response);

            // Funcion que nos permite el crear y guardar el chat en la base de datos
            var chat = new Chat 
            { 
                Provider = provider, 
                UserPrompt = promptUser, 
                BotResponse = htmlResponse,
                CreatedAT = DateTime.Now
            };
            
            await _chatRepository.AddChatAsync(chat);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}