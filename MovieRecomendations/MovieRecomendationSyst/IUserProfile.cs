using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{
    public interface IUserProfile
    {
        int Id { get; }
        string Email { get; }
        string Name { get; }
        string Surname { get; }
        WatchedMovie[] WatchedMovies { get; }

        WatchedMovie[] GetWatchedMoviesForPeriod(DateTime dateFrom, DateTime dateTo);

        double GetAverageRatingForWatchedFilms();

        double GetAverageRatingForWatchedFilms(Genres genre);

        void AddWatchedMovie(WatchedMovie watchedMovie);
    }
}
