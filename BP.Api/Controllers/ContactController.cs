using BP.Api.Models;
using BP.Api.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IConfiguration configuration, IContactService contactService, ILogger<ContactController> logger)
        {
            _configuration = configuration;
            _contactService = contactService;
            _logger = logger;
        }
        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("LogInformation - Get Method is Called");
            _logger.LogDebug("LogDebug - Get Method is Called");
            _logger.LogTrace("LogTrace - Get Method is Called");
            _logger.LogWarning("LogWarning - Get Method is Called");
            _logger.LogError("LogError - Get Method is Called");


            return _configuration["ReadMe"].ToString();
        }

        [ResponseCache(Duration = 10)]
        [HttpGet("{id}")]
        public ContactDTO GetContactById(int id)
        {
            return _contactService.GetContactById(id);
        }

        [HttpPost("create")]
        public ContactDTO CreateContact(ContactDTO contact)
        {
            //Validation işlemleri için oluşturulan metod

            return contact;
        }
    }
}
