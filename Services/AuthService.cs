using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SalesCrud.Exceptions;
using SalesCrud.Services.Interfaces;
using SalesCrud.ViewModel;

namespace SalesCrud.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<ValidationResultModel> Register(AuthSignUprViewModel model)
    {
        if (model.Password != model.ConfirmPassword)
        {
            return new ValidationResultModel(400, [new("As senhas não coincidem")]);
        }

        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, model.UserRole.ToString());
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, model.FullName));
            await _userManager.AddClaimAsync(
                user,
                new Claim(ClaimTypes.DateOfBirth, model.DateOfBirth.ToString())
            );

            return new ValidationResultModel(200, [new("Usuário criado com sucesso")]);
        }

        var errors = new List<ValidationError>();
        foreach (var error in result.Errors)
        {
            errors.Add(new ValidationError(error.Description));
        }

        return new ValidationResultModel(400, errors);
    }

    public async Task<AuthSignInRespViewModel> SignIn(AuthSignInViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email) ?? throw new DomainException("Email inválido.");
        
        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            false,
            false
        );

        if (!result.Succeeded)
            return null;

        var token = GenerateToken(user);
        var claims = await _userManager.GetClaimsAsync(user);
        var fullNameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        var fullName = fullNameClaim?.Value;

        return new AuthSignInRespViewModel
        {
            Id = user.Id,
            FullName = fullName,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<AuthSignInRespViewModel> validateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["TokenConfigurations:SecretKey"]);

        tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                },
                out SecurityToken validatedToken
        );


        var jwtToken = validatedToken as JwtSecurityToken ?? throw new DomainException("Token inválido.");
        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email) ?? throw new DomainException("Token inválido.");

        var email = emailClaim.Value;

        var user = await _userManager.FindByEmailAsync(email) ?? throw new DomainException("Token inválido.");

        var claims = await _userManager.GetClaimsAsync(user);
        var fullNameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        var fullName = fullNameClaim?.Value;

        return new AuthSignInRespViewModel
        {
            Id = user.Id,
            FullName = fullName,
            Email = user.Email,
            Token = token
        };
    }

    private string GenerateToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["TokenConfigurations:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                }
            ),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
