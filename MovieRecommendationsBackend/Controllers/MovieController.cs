using MovieRecomendationSyst;
using System.Web.Http;
using System.Net.Http;
using MovieRecommendationsBackend.Models;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Configuration;
using SimpleInjector;
using MovieRecommendationsBackend.Filters;

namespace MovieRecommendationsBackend.Controllers
{
    public class MovieController : ApiController
    {

        public MovieController(UserProfileRepository pathToRepository, IRegistrationService repository)
        {
            _pathToRepository = pathToRepository;
            _repository = repository;
        }

        UserProfileRepository _pathToRepository;
        IRegistrationService _repository;

        [ModelValidationFilter]
        [ArgumentFilter]
        [Route("addmovie/{id}")]
        public HttpResponseMessage PostFilm([FromUri]int id, [FromBody]WatchedMovieModel movie)
        {
            var allProfiles = _pathToRepository.GetAllEntities();
                var movieToAdd = new WatchedMovie(new Movie(movie.Movie.Title, movie.Movie.Director, movie.Movie.Genre), movie.Date, movie.Rating);
                for (int i = 0; i < allProfiles.Length; i++)
                {
                    if (allProfiles[i].Id == id)
                    {
                        allProfiles[i].AddWatchedMovie(movieToAdd);
                        break;
                    }
                }
                File.WriteAllText(ConfigurationManager.AppSettings["PathToRepository"], JsonConvert.SerializeObject(allProfiles.ToArray()));
                return Request.CreateResponse
                    (System.Net.HttpStatusCode.Created,
                    string.Format("Film {0} was sucessfully added to collection of user with id {1}", movie.Movie.Title, id));
        }
        [Route("getrating/{id}")]
        public HttpResponseMessage GetRating(int id)
        {
            var user = _pathToRepository.GetEntity(id);
            if (user != null)
            {
                var rating = user.GetAverageRatingForWatchedFilms();
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, string.Format("Average rating for user {0} is {1}", id, rating));
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, string.Format("User {0} not found", id));
            }
            
        }

        [Route("getrating/{genre}/{id}")]
        public HttpResponseMessage GetRating(Genres genre, int id)
        {
            var user = _pathToRepository.GetEntity(id);
            if (user != null)
            {
                var rating = user.GetAverageRatingForWatchedFilms(genre);
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, string.Format("Average rating for user {0} for genre {1} is {2}", id, genre, rating));
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, string.Format("User {0} not found", id));
            }

        }
        [Route("period/from{date1}/to{date2}/{id}")]
        public HttpResponseMessage GetMoviesForPeriod([FromUri]int id, DateTime date1, DateTime date2)
        {
            var user = _pathToRepository.GetEntity(id);
            if (user != null)
            {
                var movies = user.GetWatchedMoviesForPeriod(date1, date2);
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, movies);
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, string.Format("User {0} not found", id));
            }

        }

    }
}