using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendingEmails.Dto;
using SendingEmails.Services;

namespace SendingEmails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly IMailServices _mailServices;

        public MailsController(IMailServices mailServices)
        {
            _mailServices = mailServices;
        }

        [HttpPost("Email")]
        public async Task<IActionResult> SendMail([FromForm] EmailDto dto)
        {
            await _mailServices.SendingEmailAsync(dto.EmailTo, dto.Subject, dto.body,dto.files);
            return Ok();
        }
    }
}
