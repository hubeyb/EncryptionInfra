using EncryptionInfra.Domain.Interfaces;
using EncryptionInfra.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionInfra.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest req)
        {
            return Ok(await _authenticationService.Login(req));
        }
    }
}