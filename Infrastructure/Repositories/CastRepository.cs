using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CastRepository : EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Cast> GetByIdAsync(int id)
        {
            var cast = await _dbContext
                .Casts.Where(c => c.Id == id)
                .Include(c => c.MovieCasts)
                .ThenInclude(c => c.Movie)
                .FirstOrDefaultAsync();

            if (cast == null)
            {
                throw new Exception($"No Cast Found with {id}");
            }

            return cast;
        }
    }
}
