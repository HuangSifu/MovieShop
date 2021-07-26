﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IPurchaseRepository : IAsyncRepository<Purchase>
    {
        Task<Purchase> AddPurchase(Purchase purchase);
        Task<IEnumerable<Purchase>> GetPurchaseByUserId(int uId);
        Task<Purchase> UpdatePurchase(Purchase purchase);
    }
}
