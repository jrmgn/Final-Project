using System.Collections.Generic;
using loginpage.Models;
using loginpage.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace loginpage.Controllers
{ 
    public class AccountController : Controller
    {

        //for user login and registration
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private object signInManager;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //retrieves email and password when user login
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    isPersistent: false,        
                    lockoutOnFailure: false     
                );

                //redirect to index view (homepage with list of medicines)
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                //error prompt when wrong input for email/password
                ModelState.AddModelError("", "Email or password is incorrect.");
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Creates a new Users object from the registration model
                Users users = new Users
                {
                    FirstName = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                };
                var result = await _userManager.CreateAsync(users, model.Password);

                if (result.Succeeded)
                {
                    //redirect to index view(homepage with list of medicines)
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)

                {
                    //error prompt for registration
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            return View(model);
        }
            public async Task<IActionResult> Logout()
        {
            //signs out user
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
 }

