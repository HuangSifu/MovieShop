using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PurchaseService: IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<int> GetAllMoviesByUserId(int uId)
        {
            var movies = await _purchaseRepository.GetPurchaseByUserId(uId);
            return movies.Count();
        }

        public async Task<IEnumerable<PurchaseRequestModel>> GetPurchaseByUserId(int uid)
        {
            var movies = await _purchaseRepository.GetPurchaseByUserId(uid);
            var model = new List<PurchaseRequestModel>();
            foreach (var movie in movies)
            {
                model.Add(new PurchaseRequestModel
                {
                    MovieId = movie.MovieId,
                    PurchaseDateTime = movie.PurchaseDateTime,
                    PurchaseNumber = movie.PurchaseNumber,
                    TotalPrice = movie.TotalPrice,
                    UserId = movie.UserId
                });
            }
            return model;
        }

        public async Task<PurchaseRequestModel> Purchase(PurchaseRequestModel model)
        {
            var entity = new Purchase
            {
                PurchaseDateTime = model.PurchaseDateTime,
                MovieId = model.MovieId,
                PurchaseNumber = model.PurchaseNumber,
                TotalPrice = model.TotalPrice,
                UserId = model.UserId
            };
            await _purchaseRepository.AddPurchase(entity);
            return model;
        }

        public async Task<PurchaseRequestModel> UpdatePurchase(int pId, PurchaseRequestModel model)
        {
            var entity = new Purchase
            {
                PurchaseDateTime = model.PurchaseDateTime,
                MovieId = model.MovieId,
                PurchaseNumber = model.PurchaseNumber,
                TotalPrice = model.TotalPrice,
                UserId = model.UserId
            };
            await _purchaseRepository.UpdatePurchase(entity);
            return model;
        }
    }
}
