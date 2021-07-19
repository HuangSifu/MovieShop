using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            throw new NotImplementedException();
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



