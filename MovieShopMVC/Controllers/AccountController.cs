using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        //method for registration
        //show the empty view that will have all the text boxes and submit button
        //localhost/account/register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            //save the data only then Model Validation passes
            //if (ModelState.IsValid)
            //{
            //    //save to DB
            //}
            ////Model Binding is not case sensitive
            //return View();
            if (!ModelState.IsValid)
            {
                return View();
            }

            var craetedUser = await _userService.RegisterUser(model);
            // redirect to Login

            return RedirectToAction("Login");
            //mailkit
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userService.Login(model.Email, model.Password);

            if (user == null)
            {
                // wrong password
                ModelState.AddModelError(string.Empty, "Invalid password");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            //identity object
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //create the cookie

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return LocalRedirect("~/");
            //HttpContext
            //URL, Http method type, form body, browser, Ip address, all captured in HttpContext class

            // correct pasword
            // display, FirstName, LastName, Email
            // Button/Link Logout
            // Cookie Based Authentication....
            //10:00 AM you login website
            //created a MovieShopAuthCookie => 2 hours
            //make sure user is login successfully
            //movies/details/22 => Buy Button
            //when you click on Buy Button => POST
            //Purchase table => movieid, userid
            //user/buymovie => should take userid from cookie and send to database
            //10:15 AM you wanna buy a movie => Claims, firstname, lastname, id, email - encrypt this info and store in cookie
            //Every time you send a request from browser to server => Cookies are snt to server automatically
            //return View();
        }
    }
}
