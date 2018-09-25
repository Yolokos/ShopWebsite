using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using BookStoreWebApi.Models;
using BookStoreWebApi.ViewModel;

namespace BookStoreWebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<Customer> signInManager;

        public AccountController(
            UserManager<Customer> userManager, 
            RoleManager<IdentityRole> roleManager, 
            SignInManager<Customer> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.SurName,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await userManager.CreateAsync(customer, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customer, "user");
                    return RedirectToAction("Login", "Account");
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                 await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }
    }
}