using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using BlogApp.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BlogApp.Controllers;

public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository repository)
    {
        _userRepository = repository;
    }

    public IActionResult Login()
    {
        if(User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Index", "Posts");
        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var isUser = _userRepository.Users.FirstOrDefault(x=>x.Email == model.Email && x.Password == model.Password);
            if (isUser != null)
            {
               var userClaims = new List<Claim>();
               userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserID.ToString())); 
               userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
               userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.UserName ?? ""));
               if(isUser.Email == "info@sadikturan.com")
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                }
                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                  IsPersistent = true  
                };
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index", "Posts");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya parola yanlış");
            }
        }
        
        return View();
    }
  
}