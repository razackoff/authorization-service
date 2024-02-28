using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authorization_service.Models;
using Microsoft.IdentityModel.Tokens;

namespace authorization_service.Services.JWT;

public class JwtService : IJwtService
{
    private readonly string _jwtSecret;
    private readonly int _jwtLifetime;

    public JwtService(string jwtSecret, int jwtLifetime)
    {
        _jwtSecret = jwtSecret;
        _jwtLifetime = jwtLifetime;
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
                // Другие необходимые клеймы
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtLifetime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<User> ValidateJwtTokenAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Value;

            // Здесь вы можете загрузить пользователя из базы данных по идентификатору, указанному в токене
            // Например:
            // var user = await _userRepository.GetByIdAsync(userId);
            // return user;

            // В данном примере возвращаем только идентификатор пользователя
            return new User { Id = userId };
        }
        catch
        {
            // В случае недействительного токена или ошибки валидации возвращаем null
            return null;
        }
    }
}