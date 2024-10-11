using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderManagementService.Application.Services.Whatsapp;

namespace OrderManagementService.Test.Unit.Application
{
    [TestFixture]
    internal class WhatsappServiceTest
    {
        private Mock<IConfiguration> _configurationMock;
        private WhatsappService _whatsappService;

        [SetUp]
        public void Setup() 
        { 
            _configurationMock = new Mock<IConfiguration>();
            _whatsappService = new WhatsappService(_configurationMock.Object);
        }

        [Test]
        public void VerifyToken_ShouldReturnTrue_WhenTokensMatchAndModeIsSubscribe() 
        {
            _configurationMock.Setup(config => config["configWhatsApp:webhook:verifyToken"])
                              .Returns("expectedToken");

            string hub_mode = "subscribe";
            string hub_veryfy_token = "expectedToken";

            var result = _whatsappService.VerifyToken( hub_mode, hub_veryfy_token );   
            Assert.That( result, Is.True );
        }
    }
}
