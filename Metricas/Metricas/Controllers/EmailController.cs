using Metricas.Models;
using Metricas.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Metricas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailServices _services;

        public EmailController(EmailServices services)
        {
            _services = services;
        }

        [HttpPost]
        public Task<HttpStatusCode> SendEmail(Email email)
        {
            return _services.Send(email);
        }
    }
}
