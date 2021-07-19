using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        // DbSets as properties
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Crew> Crews { get; set; }

        // to use fluent API we need to override a nmethod OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Genre>(ConfigureGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<User>(ConfigureUser);
            //modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            //modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<Crew>(ConfigureCrew);

            modelBuilder.Entity<Movie>().HasMany(m => m.Genres).WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>("MovieGenre",
                m => m.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                g => g.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));

            modelBuilder.Entity<User>().HasMany(m => m.Roles).WithMany(g => g.Users)
                .UsingEntity<Dictionary<string, object>>("UserRole",
                m => m.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                g => g.HasOne<User>().WithMany().HasForeignKey("UserId"));
        }

        private void ConfigureGenre(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(64);
        }
        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(128);
            builder.Property(t => t.Gender).HasMaxLength(2084);
            builder.Property(t => t.TmdbUrl).HasMaxLength(2084);
            builder.Property(t => t.ProfilePath).HasMaxLength(2084);
        }
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.CastId, mc.MovieId, mc.Character});
            builder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Cast).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.CastId);
            //builder.Property(t => t.CastId);
            //builder.Property(t => t.Character).HasMaxLength(450);
        }
        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.UserId);
            builder.Property(t => t.PurchaseNumber).HasColumnType("UniqueIdentifier");
            builder.Property(t => t.TotalPrice).HasColumnType("decimal(18, 2)").HasDefaultValue(9.9m);
            builder.Property(t => t.PurchaseDateTime).HasColumnType("datetime2(7)");
            builder.Property(t => t.MovieId);
        }
        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.HasOne(mr => mr.Movie).WithMany(mr => mr.Reviews).HasForeignKey(mr => mr.MovieId);
            builder.HasOne(mc => mc.User).WithMany(mc => mc.Reviews).HasForeignKey(mc => mc.UserId);
            builder.Property(r => r.Rating).HasColumnType("decimal(3,2)").IsRequired();
            builder.Property(r => r.ReviewText).HasMaxLength(2084);
            
        }
        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.MovieId);
            builder.Property(t => t.UserId);
        }
        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FirstName).HasMaxLength(128);
            builder.Property(t => t.LastName).HasMaxLength(128);
            builder.Property(t => t.Email).HasMaxLength(256);
            builder.Property(t => t.HashedPassword).HasMaxLength(1024);
            builder.Property(t => t.Salt).HasMaxLength(1024);
            builder.Property(t => t.PhoneNumber).HasMaxLength(16);
            builder.Property(t => t.TwoFactorEnabled);
            builder.Property(t => t.LockoutEndDate).HasColumnType("datetime2(7)");
            builder.Property(t => t.LastLoginDateTime).HasColumnType("datetime2(7)");
            builder.Property(t => t.IsLocked);
            builder.Property(t => t.AccessFailedCount);
        }
        //private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        //{
        //    builder.ToTable("MovieGenre");
        //    builder.HasKey(t => t.MovieId);
        //    builder.Property(t => t.GenreId);
        //}
        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mc => new { mc.MovieId,mc.CrewId, mc.Department, mc.Job} );
            builder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCrews).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Crew).WithMany(mc => mc.MovieCrews).HasForeignKey(mc => mc.CrewId);
            //builder.Property(t => t.CrewId);
            //builder.Property(t => t.Department).HasMaxLength(128);
            //builder.Property(t => t.Job).HasMaxLength(128);
        }
        //private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        //{
        //    builder.ToTable("UserRole");
        //    builder.HasKey(t => t.UserId);
        //    builder.Property(t => t.RoleId);
        //}
        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name);
        }
        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(128);
            builder.Property(t => t.Gender).HasMaxLength(2084);
            builder.Property(t => t.ThumbUrl).HasMaxLength(2084);
            builder.Property(t => t.ProfilePath).HasMaxLength(2084);
        }
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // specify all the Fluent API rules for this model
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256).IsRequired();
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Ignore(m => m.Rating);
        }
    }
}
