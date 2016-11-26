using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{
    public class SocialisationService:ISociatisationService
    {
        public SocialisationService(UserProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public UserProfile[] GetProfilesWithSameMovies(int id)
        {
            var profilesWithSameFilms = new List<UserProfile>();
            var allProfiles = _profileRepository.GetAllEntities();
            var userWatchedMovies = GetUserWatchedMovies(allProfiles, id);
            var userWatchedMoviesTitles = GetWatchedMoviesTitles(userWatchedMovies);
            foreach (UserProfile profile in allProfiles)
            {
                if (profile.Id == id) continue;
                foreach (WatchedMovie movie in profile.WatchedMovies)
                {
                    if (userWatchedMoviesTitles.Contains(movie.Movie.Title))
                    {
                        profilesWithSameFilms.Add(profile);
                    }
                }
            }
            return profilesWithSameFilms.ToArray();
        }
        private WatchedMovie[] GetUserWatchedMovies(UserProfile[] allProfiles, int id)
        {
            foreach (var profile in allProfiles)
            {
                if (profile.Id == id)
                {
                    return profile.WatchedMovies;
                }
            }

            return new WatchedMovie[0];
        }

        private List<string> GetWatchedMoviesTitles(WatchedMovie[] watchedMovies)
        {
            var watchedMoviesTitles = new List<string>();
            foreach (var watchedMovie in watchedMovies)
            {
                watchedMoviesTitles.Add(watchedMovie.Movie.Title);
            }
            return watchedMoviesTitles;
        }

        private readonly UserProfileRepository _profileRepository;
    }
}
