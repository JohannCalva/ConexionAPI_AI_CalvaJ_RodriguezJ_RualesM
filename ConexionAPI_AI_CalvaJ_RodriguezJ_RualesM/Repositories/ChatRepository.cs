using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Models;
using ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Data;
using Microsoft.EntityFrameworkCore;

namespace ConexionAPI_AI_CalvaJ_RodriguezJ_RualesM.Repositories
{
    public class ChatRepository
    {
        private readonly SQLServerContext _context;

        public ChatRepository(SQLServerContext context)
        {
            _context = context;
        }

        public async Task<List<Chat>> GetAllChatsAsync()
        {
            return await _context.Chats.OrderByDescending(c => c.CreatedAT).ToListAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            return await _context.Chats.FindAsync(id);
        }

        public async Task AddChatAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteChatAsync(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
                return false;

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Chat>> GetChatsByProviderAsync(string provider)
        {
            return await _context.Chats
                .Where(c => c.Provider == provider)
                .OrderByDescending(c => c.CreatedAT)
                .ToListAsync();
        }
    }
}