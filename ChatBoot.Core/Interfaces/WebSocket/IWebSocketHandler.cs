using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ChatBoot.Core.Interfaces.WebSocket
{
    public interface IWebSocketHandler
    {
        Task HandleWebSocketAsync(HttpContext context, System.Net.WebSockets.WebSocket webSocket);

        Task SendMessageAsync(string message);
    }
}