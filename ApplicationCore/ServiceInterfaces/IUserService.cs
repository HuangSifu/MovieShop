using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);

        Task<UserLoginResponseModel> Login(string email, string password);

        Task<UserResponseModel> GetUserById(int id);
        Task<PurchaseResponseModel> PurchaseMovie(PurchaseRequestModel mod);

        Task<String> FavoriteMovie(UserFavoriteRequestModel model);
        Task<String> UnFavoriteMovie(UserFavoriteRequestModel model);
        Task<String> CheckUserFavorite(int id, int MovieId);

        Task<Review> ModifyReview(String text, int rating, UserLoginRequestModel model, int movieId);
        Task<Review> PutReview(Review review, UserLoginRequestModel model);
        Task<List<ReviewModel>> getReview(int id);
        Task<List<Favorite>> GetFavorite(int id);
        Task<List<Movie>> GetPurchase(int uid);
    }
}
