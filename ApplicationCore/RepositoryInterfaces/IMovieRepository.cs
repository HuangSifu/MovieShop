using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        Task<List<Movie>> GetHighest30GrossingMovies();
        Task<IEnumerable<Movie>> GetTopRatedMovies();
        Task<IEnumerable<Review>> GetReviewByMovie(int movieId);
    }
}
