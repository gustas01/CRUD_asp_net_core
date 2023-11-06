using CatalogAPI.DTOs;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase {
  private readonly UserManager<IdentityUser> _userManager;
  private readonly SignInManager<IdentityUser> _signInManager;

  public AuthorizationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration) {
    _userManager = userManager;
    _signInManager = signInManager;
  }


  [HttpGet]
  public ActionResult<string> Get() {
    return "AuthorizationController :: Acessado em : " + DateTime.Now.ToLongDateString();
  }

  [HttpPost("register")]
  public async Task<ActionResult> RegisterUser(UserDTO model) {

    var user = new IdentityUser {
      UserName = model.Email,
      Email = model.Email,
      EmailConfirmed = true,
    };

    var result = await _userManager.CreateAsync(user, model.Password);

    if(!result.Succeeded) return BadRequest(result.Errors);

    await _signInManager.SignInAsync(user, false);
    return Ok(GenerateToken(model));
  }


  [HttpPost("login")]
  public async Task<ActionResult> Login(UserDTO userInfo) {
    var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

    if(result.Succeeded) return Ok(GenerateToken(userInfo));

    else {
      ModelState.AddModelError("Errors", "Login Inválido...");
      return BadRequest(ModelState);
    }
  }


  [HttpPost]
  public UserTokenDTO GenerateToken(UserDTO user) {
    //essas claims é o payload que será adicionado no token
    var claims = new[] {
      new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
      new Claim("meuPet", "pipoca"),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    DotEnv.Load();
    IDictionary<string, string> envV = DotEnv.Read();


    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(envV["JWT_KEY"]));

    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var expiration = envV["EXPIRATION_TOKEN"];
    var expirationTime = DateTime.UtcNow.AddHours(double.Parse(expiration));

    JwtSecurityToken token = new JwtSecurityToken(
      issuer: envV["ISSUER_TOKEN"],
      audience: envV["AUDIENCE_TOKEN"],
      claims: claims,
      expires: expirationTime,
      signingCredentials: credentials
    );

    return new UserTokenDTO() {
      Authenticated = true,
      Token = new JwtSecurityTokenHandler().WriteToken(token),
      Expiration = expirationTime,
      Message = "Token JWT Ok"
    };
  }
}