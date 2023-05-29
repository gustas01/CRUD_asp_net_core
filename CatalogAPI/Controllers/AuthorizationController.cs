using CatalogAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
  private readonly UserManager<IdentityUser> _userManager;
  private readonly SignInManager<IdentityUser> _signInManager; 
  
  public AuthorizationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
  {
    _userManager = userManager;
    _signInManager = signInManager;
  }


  [HttpGet]
  public ActionResult<string> Get(){
    return "AuthorizationController :: Acessado em : " + DateTime.Now.ToLongDateString();
  }

  [HttpPost("register")]
  public async Task<ActionResult> RegisterUser(UserDTO model){

    var user = new IdentityUser {
      UserName = model.Email,
      Email = model.Email,
      EmailConfirmed = true
    };

    var result = await _userManager.CreateAsync(user, model.Password);

    if(!result.Succeeded) return BadRequest(result.Errors);

    await _signInManager.SignInAsync(user, false);
    return Ok();
  }


  [HttpPost("login")]
  public async Task<ActionResult> Login(UserDTO userInfo){
    var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);

    if(result.Succeeded) return Ok();

    else {
      ModelState.AddModelError(string.Empty, "Login Inválido...");
      return BadRequest(ModelState);
    }
  }
}