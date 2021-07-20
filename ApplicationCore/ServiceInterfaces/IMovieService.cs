using System;
using System.Collections.Generic;
using ApplicationCore.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieCardResponseModel>> GetMoviesByGenreId(int id);
        Task<List<MovieCardResponseModel>> GetMovieAsync();
        Task<List<MovieCardResponseModel>> GetTopRatedMovies();
        Task<List<ReviewResponseModel>> GetReviewsByMovie(int movieId);
    }
}
