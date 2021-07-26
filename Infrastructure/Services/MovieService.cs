using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetMovieAsync()
        {
            var movies = await _movieRepository.ListAllAsync();
            var movieCard = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCard.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                    Budget = (decimal)movie.Budget
                });
            }
            return movieCard;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetHighest30GrossingMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach(var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel 
                { 
                    Id = movie.Id, 
                    Budget = movie.Budget.GetValueOrDefault(), 
                    Title = movie.Title, 
                    PosterUrl = movie.PosterUrl 
                });
            }
            return movieCards;
            //var movies = new List<MovieCardResponseModel> {

            //              new MovieCardResponseModel {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
            //              new MovieCardResponseModel {Id = 2, Title = "Avatar", Budget = 1200000},
            //              new MovieCardResponseModel {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
            //              new MovieCardResponseModel {Id = 4, Title = "Titanic", Budget = 1200000},
            //              new MovieCardResponseModel {Id = 5, Title = "Inception", Budget = 1200000},
            //              new MovieCardResponseModel {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
            //              new MovieCardResponseModel {Id = 7, Title = "Interstellar", Budget = 1200000},
            //              new MovieCardResponseModel {Id = 8, Title = "Fight Club", Budget = 1200000},
            //};

            //return movies;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            var movieDetails = new MovieDetailsResponseModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget.GetValueOrDefault(),
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                Revenue = movie.Revenue,
            };

            movieDetails.Casts = new List<CastResponseModel>();

            foreach (var cast in movie.MovieCasts)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    Character = cast.Character,
                    ProfilePath = cast.Cast.ProfilePath,
                    TmdbUrl = cast.Cast.TmdbUrl,
                });
            }

            movieDetails.Genres = new List<GenreModel>();
            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(
                    new GenreModel
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    }
                    );
            }

            return movieDetails;
        }

        public async Task<List<MovieCardResponseModel>> GetMoviesByGenreId(int id)
        {
            var genre = await _movieRepository.GetByIdAsync(id);

            if (genre == null)
            {
                return null;
            }

            var genreMovie = new List<MovieCardResponseModel>();

            foreach (var movieGenre in genre.Genres)
            {
                genreMovie.Add(new MovieCardResponseModel
                {
                    Id = movieGenre.Id,
                    Title = genre.Title,
                    PosterUrl = genre.PosterUrl,
                    Budget = (decimal)genre.Budget,
                    
                });
            }

            return genreMovie;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetTopRatedMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }
            return movieCards;
        }

        public async Task<List<ReviewResponseModel>> GetReviewsByMovie(int movieId)
        {
            var reviews = await _movieRepository.GetReviewByMovie(movieId);
            var reviewCards = new List<ReviewResponseModel>();

            foreach (var review in reviews)
            {
                reviewCards.Add(new ReviewResponseModel
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                });

            }

            return reviewCards;
        }

        public async Task<CreateMovieRequestModel> CreateMovie(CreateMovieRequestModel createMovieRequestModel)
        {
            var movie = new Movie
            {
                Id = createMovieRequestModel.Id,
                Title = createMovieRequestModel.Title,
                PosterUrl = createMovieRequestModel.PosterUrl,
                BackdropUrl = createMovieRequestModel.BackdropUrl,
                Overview = createMovieRequestModel.Overview,
                Tagline = createMovieRequestModel.Tagline,
                Budget = createMovieRequestModel.Budget,
                Revenue = createMovieRequestModel.Revenue,
                ImdbUrl = createMovieRequestModel.ImdbUrl,
                TmdbUrl = createMovieRequestModel.TmdbUrl,
                ReleaseDate = createMovieRequestModel.ReleaseDate,
                RunTime = createMovieRequestModel.RunTime,
                Price = createMovieRequestModel.Price,
                OriginalLanguage = createMovieRequestModel.OriginalLanguage,
                //CreatedDate = createMovieRequestModel.CreatedDate,
                //UpdatedDate = createMovieRequestModel.UpdatedDate,
                //UpdatedBy = createMovieRequestModel.UpdatedBy,
                //CreatedBy = createMovieRequestModel.CreatedBy
            };
            var create = await _movieRepository.AddAsync(movie);
            var createmovie = new CreateMovieRequestModel
            {
                Id = create.Id,
                Title = create.Title,
                PosterUrl = create.PosterUrl,
                BackdropUrl = create.BackdropUrl,
                Overview = create.Overview,
                Tagline = create.Tagline,
                Budget = create.Budget,
                Revenue = (decimal)create.Revenue,
                ImdbUrl = create.ImdbUrl,
                TmdbUrl = create.TmdbUrl,
                ReleaseDate = (DateTime)create.ReleaseDate,
                RunTime = (int)create.RunTime,
                Price = (decimal)create.Price,
                OriginalLanguage = create.OriginalLanguage,
                //CreatedDate = create.CreatedDate,
                //UpdatedDate = create.UpdatedDate,
                //UpdatedBy = create.UpdatedBy,
                //CreatedBy = create.CreatedBy
            };
            createmovie.Genres = createMovieRequestModel.Genres;
            //createmovie.Casts = createMovieRequestModel.Casts;
            return createmovie;
        }
        public async Task<CreateMovieRequestModel> UpdateMovie(CreateMovieRequestModel createMovieRequestModel)
        {
            var movie = new Movie
            {
                Id = createMovieRequestModel.Id,
                Title = createMovieRequestModel.Title,
                PosterUrl = createMovieRequestModel.PosterUrl,
                BackdropUrl = createMovieRequestModel.BackdropUrl,
                Overview = createMovieRequestModel.Overview,
                Tagline = createMovieRequestModel.Tagline,
                Budget = createMovieRequestModel.Budget,
                Revenue = createMovieRequestModel.Revenue,
                ImdbUrl = createMovieRequestModel.ImdbUrl,
                TmdbUrl = createMovieRequestModel.TmdbUrl,
                ReleaseDate = createMovieRequestModel.ReleaseDate,
                RunTime = createMovieRequestModel.RunTime,
                Price = createMovieRequestModel.Price,
                OriginalLanguage = createMovieRequestModel.OriginalLanguage,
                //CreatedDate = createMovieRequestModel.CreatedDate,
                //UpdatedDate = createMovieRequestModel.UpdatedDate,
                //UpdatedBy = createMovieRequestModel.UpdatedBy,
                //CreatedBy = createMovieRequestModel.CreatedBy
            };
            var create = await _movieRepository.AddAsync(movie);
            var createmovie = new CreateMovieRequestModel
            {
                Id = create.Id,
                Title = create.Title,
                PosterUrl = create.PosterUrl,
                BackdropUrl = create.BackdropUrl,
                Overview = create.Overview,
                Tagline = create.Tagline,
                Budget = create.Budget,
                Revenue = (decimal)create.Revenue,
                ImdbUrl = create.ImdbUrl,
                TmdbUrl = create.TmdbUrl,
                ReleaseDate = (DateTime)create.ReleaseDate,
                RunTime = (int)create.RunTime,
                Price = (decimal)create.Price,
                OriginalLanguage = create.OriginalLanguage,
                //CreatedDate = create.CreatedDate,
                //UpdatedDate = create.UpdatedDate,
                //UpdatedBy = create.UpdatedBy,
                //CreatedBy = create.CreatedBy
            };
            createmovie.Genres = createMovieRequestModel.Genres;
            //createmovie.Casts = createMovieRequestModel.Casts;
            return createmovie;
        }

        public async Task<PurchaseResponseModel> GetMoviePurchases(PurchaseResponseModel purchaseResponseModel)
        {
            throw new NotImplementedException();
        }
    }

        
    }
    


    //public class MovieService2 : IMovieService
    //{
    //    public List<MovieCardResponseModel> GetTopRevenueMovies()
    //    {
    //        var movies = new List<MovieCardResponseModel> {

    //                      new MovieCardResponseModel {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
    //                      new MovieCardResponseModel {Id = 2, Title = "Avatar", Budget = 1200000},

    //                      new MovieCardResponseModel {Id = 8, Title = "Fight Club", Budget = 1200000},
    //        };

    //        return movies;
    //    }
    //}



