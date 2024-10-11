using OrderManagementService.Core.Interfaces.Whatsapp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderManagementService.API.Controllers.v1
{
    [ApiVersion("1.0")]
    public class WhatsappController : BaseController
    {
        private readonly IWhatsappService _whatsappService;

        public WhatsappController(IWhatsappService whatsappService) 
        {
            _whatsappService = whatsappService; 
        }

        [HttpGet]
        public IActionResult Webhook([FromQuery(Name = "hub.mode")] string hub_mode, [FromQuery(Name = "hub.challenge")] string hub_challenge, [FromQuery(Name = "hub.verify_token")] string hub_verify_token)
        {
            if (_whatsappService.VerifyToken(hub_mode, hub_verify_token))
                return Ok(hub_challenge);

            return StatusCode(403);
        }
    }
}
