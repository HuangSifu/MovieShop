using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        [HttpGet]
        public IActionResult BuyMovie(int mid)
        {
            var purchaseMovie = new PurchaseRequestModel
            {
                MovieId = mid,
                UserId = _currentUser.UserId,
                /*TotalPrice = _movieService.GetMovieDetails(mid)*/
                TotalPrice = 0,/*MovieRepository.(_movieRepository.GetByIdAsync(mid)).Price.GetType().Name;*/
                //TotalPrice = _movieService.GetMovieDetails(mid),
            };

            return View(purchaseMovie);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmPurchase(PurchaseRequestModel model)
        {
            if (!_currentUser.IsAuthenticated)
            {
                return LocalRedirect("~/Account/Login");
            }
            var purchase = new Purchase
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                PurchaseDateTime = DateTime.Now,
                PurchaseNumber = Guid.NewGuid(),
                TotalPrice = model.TotalPrice
            };

            await _buyRepository.AddAsync(purchase);

            return LocalRedirect("~/");


        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
