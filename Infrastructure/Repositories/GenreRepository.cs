using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            var genres = await _dbContext.Genres.ToListAsync();
            return genres;
        }

        public override async Task<Genre> GetByIdAsync(int id)
        {
            var genre = await _dbContext
                .Genres.Include(g => g.Movies).FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                throw new Exception($"No Genre Found with {id}");
            }

            return genre;
        }
    }
}
