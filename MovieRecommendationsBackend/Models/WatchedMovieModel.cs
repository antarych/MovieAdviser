using MovieRecomendationSyst;
using System;

namespace MovieRecommendationsBackend.Models
{
    public class WatchedMovieModel
    {
        public WatchedMovieModel(Movie movie, DateTime date, int rating)
        {
            Movie = movie;
            Date = date;
            Rating = rating;
        }
        public Movie Movie { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
    }
}