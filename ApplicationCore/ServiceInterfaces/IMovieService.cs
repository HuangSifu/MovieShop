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
        List<MovieCardResponseModel> GetTopRevenueMovies();
    }
}
