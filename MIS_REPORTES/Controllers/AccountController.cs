using Microsoft.AspNetCore.Mvc;
using MIS_REPORTES.ViewsModel;
using MIS_REPORTES.Service;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.Authenticate(model.User, model.Password);
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
        }

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Logout()

    {
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.Register(model.User, model.Password, model.Email);

            if (result)
            {
                // Registro exitoso, iniciar sesión automáticamente y redirigir
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "El usuario ya existe.");
        }

        return View(model);
    }
}
