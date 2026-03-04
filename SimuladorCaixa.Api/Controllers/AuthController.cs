using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimuladorCaixa.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration config, ILogger<AuthController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost("token")]
        public IActionResult GerarToken([FromBody] LoginRequest request)
        {

            _logger.LogInformation("Tentativa de autenticacao para usuario {Usuario}", request.Usuario);

            if (request.Usuario != "admin" || request.Senha != "admin")
            {
                _logger.LogWarning("Falha de autenticacao para usuario {Usuario}", request.Usuario);
                return Unauthorized();
            }

            var jwt = _config.GetSection("Jwt");
            var issuer = jwt["Issuer"];
            var audience = jwt["Audience"];
            var key = jwt["Key"]!;

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, request.Usuario),
            new Claim("role", "admin")
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            _logger.LogInformation("Token gerado com sucesso para usuario {Usuario}",request.Usuario);

            return Ok(new { token = tokenString });
        }

        public sealed record LoginRequest(string Usuario, string Senha);
    }
}
