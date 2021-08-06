using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> CheckUserFavorite(int id, int MovieId)
        {
            var res = await _userRepository.CheckFavorite(id, MovieId);
            return res;
        }

        public async Task<string> FavoriteMovie(UserFavoriteRequestModel model)
        {
            var res = await _userRepository.Favorite(model);
            return res;
        }

        public async Task<List<Favorite>> GetFavorite(int id)
        {
            return await _userRepository.GetFavorites(id);
        }

        public async Task<List<Movie>> GetPurchase(int uid)
        {
            var res = await _userRepository.RepositoryGetPurchase(uid);
            return res;
        }

        public async Task<List<ReviewModel>> getReview(int id)
        {
            return await _userRepository.GetReviews(id);
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var userResponseModel = new UserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return userResponseModel;
        }

        public async Task<UserLoginResponseModel> Login(string email, string password)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                throw new NotFoundException("Email does not exists, please register first");
            }

            var hashedPssword = HashPassword(password, dbUser.Salt);

            if (hashedPssword == dbUser.HashedPassword)
            {
                // good, correct password

                var userLoginRespone = new UserLoginResponseModel
                {

                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    DateOfBirth = dbUser.DateOfBirth,
                    LastName = dbUser.LastName
                };

                return userLoginRespone;
            }

            return null;
        }

        public async Task<Review> ModifyReview(string text, int rating, UserLoginRequestModel model, int movieId)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser == null)
            {
                throw new NotFoundException("Email does not exists, please register first");
            }

            var hashedPssword = HashPassword(model.Password, dbUser.Salt);

            if (hashedPssword != dbUser.HashedPassword)
            {
                throw new NotFoundException("Password inccorect");
            }

            var user = await _userRepository.GetUserByEmail(model.Email);
            var review = new Review { MovieId = movieId, Rating = rating, ReviewText = text, UserId = user.Id};
            var res = await _userRepository.PutReview(review, user.Id);
            return res;
        }

        public async Task<PurchaseResponseModel> PurchaseMovie(PurchaseRequestModel model)
        {
            //var dbUser = await _userRepository.GetUserByEmail(model.Email);
            //if (dbUser == null)
            //{
            //    throw new NotFoundException("Email does not exists, please register first");
            //}

            //var hashedPssword = HashPassword(model.Password, dbUser.Salt);

            //if (hashedPssword != dbUser.HashedPassword)
            //{
            //    throw new NotFoundException("Password inccorect");
            //}
            var purchase = await _userRepository.PurchaseMovie(model);
            return new PurchaseResponseModel { PurchaseNumber = purchase.PurchaseNumber };
        }

        public async Task<Review> PutReview(Review review, UserLoginRequestModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser == null)
            {
                throw new NotFoundException("Email does not exists, please register first");
            }

            var hashedPssword = HashPassword(model.Password, dbUser.Salt);

            if (hashedPssword != dbUser.HashedPassword)
            {
                throw new NotFoundException("Password inccorect");
            }
            var user = await _userRepository.GetUserByEmail(model.Email);
            var res = await _userRepository.PutReview(review, user.Id);
            return res;
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // Make sure email does not exists in database User table

            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);

            if (dbUser != null)
            {
                // we already have user with same email
                throw new ConflictException("Email arleady exists");
            }

            // create a unique salt

            var salt = CreateSalt();

            var hashedPassword = HashPassword(requestModel.Password, salt);

            // save to database

            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                HashedPassword = hashedPassword
            };

            // save to database by calling UserRepository Add method
            var createdUser = await _userRepository.AddAsync(user);

            var userResponse = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userResponse;
        }

        public async Task<string> UnFavoriteMovie(UserFavoriteRequestModel model)
        {
            var res = await _userRepository.UnFavorite(model);
            return res;
        }

        private string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string HashPassword(string password, string salt)
        {
            //Pbkdf2
            //Aarogon
            //BCrypt
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                    password: password,
                                                                    salt: Convert.FromBase64String(salt),
                                                                    prf: KeyDerivationPrf.HMACSHA512,
                                                                    iterationCount: 10000,
                                                                    numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}