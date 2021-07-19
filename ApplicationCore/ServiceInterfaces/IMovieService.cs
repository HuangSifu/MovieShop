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
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);
    }
}
