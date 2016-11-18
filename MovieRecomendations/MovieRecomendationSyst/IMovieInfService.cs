using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{
    interface IMovieInfService
    {
        double GetAverageRating(int id);

        double GetAverageRating(int id, Genres genre);

        WatchedMovie[] GetMoviesForPeriod(int id, DateTime dateFrom, DateTime dateTo);
    }
}
