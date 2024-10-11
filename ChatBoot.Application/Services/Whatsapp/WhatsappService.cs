using OrderManagementService.Core.Interfaces.Whatsapp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementService.Application.Services.Whatsapp
{
    public class WhatsappService: IWhatsappService
    {
        private readonly IConfiguration _configuration;

        public WhatsappService(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }

        public bool VerifyToken(string hub_mode, string hub_veryfy_token)
        {
            var verifyToken = _configuration["configWhatsApp:webhook:verifyToken"];
            return hub_mode == "subscribe" && hub_veryfy_token == verifyToken;

        }
    }
}
