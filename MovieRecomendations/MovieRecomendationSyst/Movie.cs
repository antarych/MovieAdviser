using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{
    public enum Genres
    {
        thriller, detective, documentary, horror, cartoon, comedy, drama, melodrama,
        crime, mystic, musical, scientific, adventure, fiction, fantasy
    };
    public class Movie
    {
        public Movie(string title, string director, Genres genre)
        {
            Title = title;
            Director = director;
            Genre = genre;
        }

        public string Title { get; set; }
        public string Director { get; set; }
        public Genres Genre { get; set; }
    }
}
