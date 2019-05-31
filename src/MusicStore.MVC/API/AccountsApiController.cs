using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicStore.MVC.Dto;
using MusicStore.MVC.Models;
using MusicStore.MVC.Services;

namespace MusicStore.MVC.API
{
  [ApiController]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
  public class AccountsApiController : ControllerBase
  {
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly TokenSettings tokenSetting;

    public AccountsApiController(
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      IPasswordHasher<User> passwordHasher,
      IOptions<AppSettings> config
      )
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.passwordHasher = passwordHasher;
      this.tokenSetting = config.Value.Token;
    }
    [AllowAnonymous]
    [HttpPost("api/auth/token")]
    public async Task<IActionResult> GenerateToken([FromBody]UserLogginDto model)
    {
      if (ModelState.IsValid != true) return BadRequest(ModelState.Values);

      // Check userName and password
      var user = await this.userManager.FindByEmailAsync(model.Email);
      if (user == null) return BadRequest("Wrong Credential!");

      if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password)
      != PasswordVerificationResult.Success) return BadRequest("Wrong Credential!");
      
      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(ClaimTypes.Name, user.UserName),
      };
      // userRoles to be added to the token
      var roles = await userManager.GetRolesAsync(user);
      claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
      // verification algorithms
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSetting.Key));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      // build the token
      var token = new JwtSecurityToken(
        issuer: tokenSetting.Issuer,
        audience: tokenSetting.Audience[0],
        claims: claims,
        notBefore: DateTime.UtcNow,
        expires: DateTime.UtcNow.AddMinutes(10),
        signingCredentials: creds
      );

      return Ok(new
      {
        token = new JwtSecurityTokenHandler().WriteToken(token),
      });
    }
  }
}