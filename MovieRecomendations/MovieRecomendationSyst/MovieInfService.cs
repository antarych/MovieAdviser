using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{
    public class MovieInfService : IMovieInfService
    {
        public MovieInfService(UserProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public double GetAverageRating(int id)
        {
            return _profileRepository.GetEntity(id).GetAverageRatingForWatchedFilms();
        }

        public double GetAverageRating(int id, Genres genre)
        {
            return _profileRepository.GetEntity(id).GetAverageRatingForWatchedFilms(genre);
        }

        public WatchedMovie[] GetMoviesForPeriod(int id, DateTime dateFrom, DateTime dateTo)
        {
            return _profileRepository.GetEntity(id).GetWatchedMoviesForPeriod(dateFrom, dateTo);
        }
        private readonly UserProfileRepository _profileRepository;
    }
}
