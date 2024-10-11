using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementService.Core.Interfaces.Whatsapp
{
    public interface IWhatsappService
    {
        bool VerifyToken(string hub_mode, string hub_veryfy_token);
    }
}
