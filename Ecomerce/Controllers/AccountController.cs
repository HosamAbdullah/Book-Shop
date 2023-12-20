using Ecomerce.Models;
using Ecomerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecomerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<user> userManager;
        private readonly SignInManager<user> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<user> _userManager, SignInManager<user> _signInManager, RoleManager<IdentityRole> roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            this.roleManager = roleManager;
        }
        //register
        public IActionResult Register()
        {
            UserVM uservm = new UserVM();
            return View(uservm);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new user
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.email.Split('@')[0],
                    Email = model.email,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);

        }
        //login


        public IActionResult LogIn(string ReturnUrl = "~/Home/Index")
        {
            ViewData["redirect"] = ReturnUrl;
			LogInVM logInVM = new LogInVM();
            return View(logInVM);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM model, string ReturnUrl = "~/Home/Index")
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await userManager.CheckPasswordAsync(user, model.Password);
                    if (result)
                    {
                        var logInResult = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                        if (logInResult.Succeeded)
                            return LocalRedirect(ReturnUrl);
                        else
                            ModelState.AddModelError("", "username and password are not correct");
                        return View(model);
                    }
                }

            }
            return View(model);

        }
        //signout
        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }

        //add role 
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            RoleVM roleVM = new RoleVM();
            return View(roleVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = roleVM.name
                };
                var result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return Content("succeded");
                }
                else
                {
                    return BadRequest();
                }
            }
            return View(roleVM);
        }
        //add admin
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            UserVM uservm = new UserVM();
            return View("Register", uservm);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAdmin(UserVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new user
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.email.Split('@')[0],
                    Email = model.email,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);

        }


    }
}
