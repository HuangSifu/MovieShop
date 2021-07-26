using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseModel>> GetReviewDetailsByMovieId(int movieId);
        Task<IEnumerable<ReviewResponseModel>> GetReviewDetailsByUserId(int userId);
    }
}
