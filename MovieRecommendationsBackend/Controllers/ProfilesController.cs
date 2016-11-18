using MovieRecommendationsBackend.Models;
using System.Net.Http;
using System.Web.Http;
using MovieRecomendationSyst;
using System.Configuration;

namespace MovieRecommendationsBackend.Controllers
{
    public class ProfilesController : ApiController
    {
        static string path = ConfigurationManager.AppSettings["PathToRepository"].ToString();
        public static IRepository<UserProfile> pathToRepository = new UserProfileRepository(path);
        public RegistrationService repository = new RegistrationService(pathToRepository);
        public HttpResponseMessage PostUser([FromBody]Registration user)
        {           
            repository.AddUser(user.Email, user.Name, user.Surname);
            return Request.CreateResponse
                (System.Net.HttpStatusCode.Created,
                string.Format("User {0} was sucessfully added", user.Name));
        }

        public HttpResponseMessage GetUserProfile(int id)
        {
            var user = pathToRepository.GetEntity(id);
            if (user != null) { return Request.CreateResponse(user); }
            else
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, string.Format("User with {0} id not found", id));
        }

        public HttpResponseMessage GetAllProfiles()
        {
            var allProfiles = pathToRepository.GetAllEntities();
            if (allProfiles != null)
            {
                return Request.CreateResponse(allProfiles);
            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, string.Format("List is empty"));
            }
        }

        [Route("adviser/{id}/matches")]
        public HttpResponseMessage GetMatches(int id)
        {
            var user = pathToRepository.GetEntity(id);
            if (user != null)
            {
                SocialisationService profileRepository = new SocialisationService(pathToRepository);
                var matches = profileRepository.GetProfilesWithSameMovies(id);
                
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, matches);        
            }
            else return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, string.Format("User {0} not found", id));
        }
    }
}