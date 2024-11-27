using google_login.Server.Abtractions;
using google_login.Server.Options;
using Google_Login.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Google_Login.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IOptions<JwtOptions> jwtOptions, 
                            IGoogleLoginProvider googleLoginProvider) : Controller
{

    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
 
    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        var payload = await googleLoginProvider.ValidateTokenAsync(request.Token);
        if (payload is null)
            return BadRequest("Invalid Google Token");

        var jwtToken = GenerateJwtToken(payload);
        return Ok(new { token = jwtToken });
    }
 
    private string GenerateJwtToken(GoogleUserInfoResponse? payload)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, payload.Sub),
            new Claim(JwtRegisteredClaimNames.Email, payload.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
