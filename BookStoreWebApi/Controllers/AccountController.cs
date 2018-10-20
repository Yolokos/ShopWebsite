using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using BookStoreWebApi.Models;
using BookStoreWebApi.ViewModel;
using BookStoreWebApi.Services;
using BookStoreWebApi.FIlters;

namespace BookStoreWebApi.Controllers
{
    //This class for controll account
    //Also for register or log and logout
    //You can edit your profile and etc.
    public class AccountController : Controller
    {
        private readonly UserManager<Customer> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<Customer> signInManager;
        private readonly ApplicationDbContext db;

        public AccountController(
            UserManager<Customer> userManager, 
            RoleManager<IdentityRole> roleManager, 
            SignInManager<Customer> signInManager,
            ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            return View(await userManager.GetUserAsync(HttpContext.User));
        }

        [HttpGet]
        [AllowAnonymous]
        [AfterAuth]
        public IActionResult Register()
        {
            return View();
        }

        //not complete
        [Authorize]
        public async Task<IActionResult> Orders()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            ShoppingCart shoppingCart = await db.Shoppings.FirstOrDefaultAsync(p => p.Id == user.Id);
            List<Order> orders = await db.Orders.Where(p => p.ShoppingCartId == shoppingCart.Id).ToListAsync();
            List<Book> books = await db.Books.Where(p => p.ShoppingCartId == shoppingCart.Id).ToListAsync();
           
            
            return View(new CurrentOrdersViewModel { Orders = orders, Books = books});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    SurName = model.SurName,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await userManager.CreateAsync(customer, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(customer, "user");
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(customer);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { customerId = customer.Id, code = code },
                        protocol: HttpContext.Request.Scheme);
                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string customerId, string code)
        {
            if (customerId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await userManager.FindByIdAsync(customerId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AfterAuth]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl});
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;
            if (ModelState.IsValid)
            {
                var customer = await userManager.FindByNameAsync(model.Email);
                if (customer != null)
                {
                    // проверяем, подтвержден ли email
                    if (!await userManager.IsEmailConfirmedAsync(customer))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                        return View(model);
                    }
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                                    model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logoff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}