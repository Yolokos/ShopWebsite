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
        public async Task<IActionResult> ChangePassword(string id) {
            Customer customer = await userManager.FindByIdAsync(id);
            if(customer != null)
            {
                ChangePasswordViewModel changePassword = new ChangePasswordViewModel { Id = customer.Id};
                return View(changePassword);
            }
            return NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Reset Password",
                    $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AfterAuth]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AfterAuth]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        [AfterAuth]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await userManager.FindByIdAsync(model.Id);
                if(customer != null)
                {
                    var result = await userManager.ChangePasswordAsync(customer, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile", "Account");
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");  
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfile profile)
        {
            Customer user = await userManager.GetUserAsync(HttpContext.User);
            if (profile != null)
            {
                user.Name = profile.Name;
                user.SurName = profile.SurName;
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Profile", "Account");
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

            List<Order> orderBooks = await db.Orders.Include(c => c.OrderBooks).ThenInclude(p => p.Book).Where(p => p.CustomerId == user.Id).ToListAsync();


            return View(orderBooks);
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
            return View(new LoginViewModel { ReturnUrl = returnUrl });
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