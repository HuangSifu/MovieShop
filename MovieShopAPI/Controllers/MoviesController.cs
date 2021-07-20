using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // attribute based routing

        //toprevenue API
        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }

            return Ok(movies);

        }

        //getbygenreid API
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int id)
        {
            var movies = await _movieService.GetMoviesByGenreId(id);
            if (movies.Any())
            {
                return Ok(movies);
            }
            return NotFound("No movies found");
        }

        //GetMovieId API
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if (movie == null)
            {
                return NotFound($"No Movie Found for that {id}");
            }
            return Ok(movie);
        }

        //GetAllMoviesAPI
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMovieAsync()
        {
            var movies = await _movieService.GetMovieAsync();
            if (movies.Any())
            {
                return Ok(movies);
            }
            return NotFound("No movie found");
        }

        //GetTopRated API
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTopRatedAsync()
        {
            var movies = await _movieService.GetTopRatedMovies();
            if (!movies.Any())
            {
                return NotFound("No movie found");
            }
            return Ok(movies);
        }

        //GetMovieReviews API
        [HttpGet]
        [Route("{movieId:int}")]
        public async Task<IActionResult> GetReviewsByMovie(int movieId)
        {
            var reviews = await _movieService.GetReviewsByMovie(movieId);

            if (reviews.Any())
            {
                return Ok(reviews);
            }

            return NotFound("Review not found");
        }
    }
}
