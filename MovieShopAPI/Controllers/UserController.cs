using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPurchaseService _purchaseService;
        private readonly IReviewService _reviewService;
        private readonly IReviewRepository _reviewRepository;

        public UserController(IUserService userService, IReviewService reviewService, IPurchaseService purchaseService,
            IReviewRepository reviewRepository)
        {
            _reviewService = reviewService;
            _purchaseService = purchaseService;
            _userService = userService;
            _reviewRepository = reviewRepository;
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> AddPurchase(PurchaseRequestModel model)
        {
            var purchase = await _purchaseService.Purchase(model);
            return CreatedAtRoute("GetPurchaseByUserId", model.UserId, purchase);
        }

        [HttpGet("{id}/purchases", Name = "GetPurchaseByUserId")]
        public async Task<IActionResult> GetPurchaseByUserId([FromRoute] int id)
        {
            var purchase = await _purchaseService.GetPurchaseByUserId(id);
            if (purchase == null) return NotFound("This user have never bought any movie");
            return Ok(purchase);
        }

        [HttpPost("review")]
        public async Task<IActionResult> AddReview(Review review)
        {
            _reviewRepository.AddReviewWithUserId(review.UserId, review);
            await _reviewRepository.SaveAsync();
            return NoContent();
        }

        [HttpPut("review")]
        public async Task<IActionResult> UpdateReview(Review review)
        {
            _reviewRepository.UpdateReviewWithUserId(review.UserId, review);
            await _reviewRepository.SaveAsync();
            return NoContent();
        }

        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviewsWithUserId(int id)
        {
            var model = await _reviewService.GetReviewDetailsByUserId(id);
            if (model == null) return NotFound("No Review in this User");

            return Ok(model);
        }
    }
}
