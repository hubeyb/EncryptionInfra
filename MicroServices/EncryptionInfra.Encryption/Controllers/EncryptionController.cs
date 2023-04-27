using EncryptionInfra.Business.Services;
using EncryptionInfra.Domain.Interfaces;
using EncryptionInfra.Domain.Model;
using EncryptionInfra.Encryption.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionInfra.Encryption.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TokenAuthorize]
    public class EncryptionController : ControllerBase
    {
        private readonly ILogger<EncryptionController> _logger;
        private EncryptionProducerService _encryptionProducerService;

        public EncryptionController(ILogger<EncryptionController> logger, EncryptionProducerService encryptionProducerService)
        {
            _logger = logger;
            _encryptionProducerService = encryptionProducerService;
        }

        [HttpPost("encrypt")]
        public async Task<IActionResult> Encrypt([FromBody] EncryptionRequest req)
        {
            return Ok(_encryptionProducerService.SendEncrypt(new EncryptionText() { Text = req.PlainText }));
        }

        [HttpPost("decrypt")]
        public async Task<IActionResult> Decrypt([FromBody] DecryptionRequest req)
        {
            return Ok(_encryptionProducerService.SendDecrypt(new DecryptionText() { Text = req.EncryptedText }));
        }
    }
}