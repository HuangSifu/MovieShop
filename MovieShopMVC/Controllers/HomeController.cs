using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            //var myType = movies.GetType();
            //ViewBag.MoviesCount = movies.Count();
            return View(movies);
            //3 ways to send the data from Controller/action to View
            //1. *** Models (stronly typed models)
            //2. 
            //3. 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetHighestGrossingMovies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//localhost/user/purchases

//localhost/admin/createmovie =>
//1. User is loged in and role of admin
//localhost/admin/createcast
//localhost/admin/recenttoppurchases

//1. Register the user
//email, password, firstname, lastname, dob
//save user info into user table
//check if email already =>
//Validation => email is in right format, max length => first name and last name should not be empty
//DOB => Minimum age 15
//Password should be strong, 1 Capital, 1 Number, 1 Special charater and min 8
//POST => localhost/account/register
// Terminal, Postman, C#(HttpClient), Java, JS
//password: PI information 
// encryption: means it can be Decrypt back to original string
//Hashing the password: One Way => 
// u1 => Abx123!! Hash1 => dafgasdfghfhdsa(No dehash thing)
//u2 => Abx123!! Hash1 => dafgasdfghfhdsa
//Better one: Hashing with Salt(Unique random string)
//u1 => (Abx123!! + fhladfsjvlsxcjv) Hash1 => aklsduhfgilueqahbrguiygb
//u2 => (Abx123!! + wqyuefgzlujxycg) Hash1 => sadfiguhieoaqrhgiuhbnzcx

//Login Page
//Email/PW button =>
//a@a.com Abx123!! => (passord form user (UI) + Salt from database) => compare this hash with database hash
//salt is always different