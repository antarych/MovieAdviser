using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{

    public class WatchedMovie
    {
        public WatchedMovie(Movie movie, DateTime date, int rating)
        {
            Movie = movie;
            Date = date;
            Rating = rating;
        }
        public Movie Movie { get; private set; }
        public DateTime Date { get; private set; }
        public int Rating { get; private set; }
    }
}
