using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{
    public class UserProfile:IUserProfile
    {
        public UserProfile(int id, string email, string name, string surname, WatchedMovie[] watchedMovies)
        {
            Id = id;
            Email = email;
            Name = name;
            Surname = surname;
            _watchedMovies = new List<WatchedMovie>(watchedMovies ?? new WatchedMovie[0]);
        }
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public WatchedMovie[] WatchedMovies
        {
            get
            {
                return _watchedMovies.ToArray();
            }
        }
        public WatchedMovie[] GetWatchedMoviesForPeriod(DateTime dateFrom, DateTime dateTo)
        {
            var moviesForPeriod = new List<WatchedMovie>();
            foreach (WatchedMovie movie in _watchedMovies)
            {
                if (movie.Date >= dateFrom && movie.Date <= dateTo)
                {
                    moviesForPeriod.Add(movie);
                }
            }
            return moviesForPeriod.ToArray();
        }

        public double GetAverageRatingForWatchedFilms()
        {
            double ratingSum = 0.0;
            foreach (WatchedMovie movie in _watchedMovies)
            {
                ratingSum += movie.Rating;
            }
            return ratingSum / _watchedMovies.Count;
        }

        public double GetAverageRatingForWatchedFilms(Genres genre)
        {
            double ratingSum = 0.0;
            int count = 0;
            foreach (WatchedMovie movie in _watchedMovies)
            {
                if (movie.Movie.Genre == genre)
                {
                    ratingSum += movie.Rating;
                    count++;
                }
            }
            return ratingSum / count;
        }

        public void AddWatchedMovie(WatchedMovie movie)
        {
            _watchedMovies.Add(movie);            
        }
        private List<WatchedMovie> _watchedMovies;
    }
}
