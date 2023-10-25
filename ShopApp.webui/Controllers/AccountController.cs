using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.webui.EmailServices;
using ShopApp.webui.Identity;
using ShopApp.webui.Models;

namespace ShopApp.webui.Controllers
{
    [AutoValidateAntiforgeryToken] // XSS i önler
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,IEmailSender emailsender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailsender;
        }


        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            { 
                ReturnUrl = ReturnUrl
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("",$"No account with {model.Email} mail address!");
                    return View(model);
                }
                
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("","Please confirm your account by email!");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user,model.Password,false,false);

                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl??"~/");
                }
                
                ModelState.AddModelError("","Email or password is wrong!");
                return View(model);
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user,model.Password);

            if (result.Succeeded)
            {
                //generate token
                var mytoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = Url.Action("ConfirmEmail","Account",new{

                    userId = user.Id,
                    token = mytoken
                });
                Console.WriteLine(url);

                //Email 
                await _emailSender.EmailSenderAsyc(model.Email,"Confirm your account.",$"Please <a href='https://localhost:5001{url}'>click link<a> for confirm your email address.");
                return RedirectToAction("Login","Account");
            }

            ModelState.AddModelError("", result.Errors.First().Description);

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if (userId == null || token == null)
            {
                TempData["message"] = "Invalid token!";
                return View();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user,token);

                if (result.Succeeded)
                {
                    TempData["message"] = "Account confirmed!";
                    return View();
                }
            }

            TempData["message"] = "Your account not confirmed!";
            return View();

        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            //generate token
            var url = Url.Action("ResetPassword","Account",new{

                userId = user.Id,
                token = code
            });
            Console.WriteLine(url);

            //Email 
            await _emailSender.EmailSenderAsyc(Email,"Reset Password.",$"Please <a href='https://localhost:5001{url}'>click link<a> for reset your password.");
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Home","Index");
            }

            var model = new ResetPasswordModel {Token = token};

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return RedirectToAction("Home","Index");
            }

            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }

            return View(model);
        }

    }
}