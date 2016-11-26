using MovieRecomendationSyst;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieRecommendationsBackend.Models
{
    public class WatchedMovieModel
    {
        [Required]
        public Movie Movie { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Range(1, 10)]
        [Required]
        public int Rating { get; set; }
    }
}