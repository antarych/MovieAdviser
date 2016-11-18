﻿using MovieRecomendationSyst;
using System.Web.Http;
using System.Net.Http;
using MovieRecommendationsBackend.Models;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Configuration;

namespace MovieRecommendationsBackend.Controllers
{
    public class MovieController : ApiController
    {
        static string path = ConfigurationManager.AppSettings["PathToRepository"].ToString();
        public static IRepository<UserProfile> pathToRepository = new UserProfileRepository(path);
        public RegistrationService repository = new RegistrationService(pathToRepository);
        [Route("addmovie/{id}")]
        public HttpResponseMessage PostFilm([FromUri]int id, [FromBody]WatchedMovieModel movie)
        {
            var allProfiles = pathToRepository.GetAllEntities();
            if (movie != null)
            {
                var movieToAdd = new WatchedMovie(new Movie(movie.Movie.Title, movie.Movie.Director, movie.Movie.Genre), movie.Date, movie.Rating);
                for (int i = 0; i < allProfiles.Length; i++)
                {
                    if (allProfiles[i].Id == id)
                    {
                        allProfiles[i].AddWatchedMovie(movieToAdd);
                        break;
                    }
                }
                File.WriteAllText(path, JsonConvert.SerializeObject(allProfiles.ToArray()));
                return Request.CreateResponse
                    (System.Net.HttpStatusCode.Created,
                    string.Format("Film {0} was sucessfully added to collection of user with id {1}", movie.Movie.Title, id));
            }
            else
            {
                return Request.CreateResponse
                    (System.Net.HttpStatusCode.BadRequest,
                    string.Format("Bad Request", id));
            }
        }
        [Route("getrating/{id}")]
        public HttpResponseMessage GetRating(int id)
        {
            var user = pathToRepository.GetEntity(id);
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
            var user = pathToRepository.GetEntity(id);
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
            var user = pathToRepository.GetEntity(id);
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