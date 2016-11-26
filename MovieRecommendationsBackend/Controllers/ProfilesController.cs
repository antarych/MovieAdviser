using MovieRecommendationsBackend.Models;
using System.Net.Http;
using System.Web.Http;
using MovieRecomendationSyst;
using System.Configuration;
using MovieRecommendationsBackend.App_Start;
using SimpleInjector;
using MovieRecommendationsBackend.Filters;

namespace MovieRecommendationsBackend.Controllers
{
    public class ProfilesController : ApiController
    {

        public ProfilesController(UserProfileRepository pathToRepository, RegistrationService repository)
        {
            _pathToRepository = pathToRepository;
            _repository = repository;
        }

        UserProfileRepository _pathToRepository;
        RegistrationService _repository;
        

        [ArgumentFilter]
        [ModelValidationFilter]
        public HttpResponseMessage PostUser([FromBody]Models.Registration user)
        {           
            _repository.AddUser(user.Email, user.Name, user.Surname);
            return Request.CreateResponse
                (System.Net.HttpStatusCode.Created,
                string.Format("User {0} was sucessfully added", user.Name));
        }

        public HttpResponseMessage GetUserProfile(int id)
        {
            var user = _pathToRepository.GetEntity(id);
            if (user != null) { return Request.CreateResponse(user); }
            else
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, string.Format("User with {0} id not found", id));
        }

        public HttpResponseMessage GetAllProfiles()
        {
            var allProfiles = _pathToRepository.GetAllEntities();
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
            var user = _pathToRepository.GetEntity(id);
            if (user != null)
            {
                SocialisationService profileRepository = new SocialisationService(_pathToRepository);
                var matches = profileRepository.GetProfilesWithSameMovies(id);
                
                return Request.CreateResponse(System.Net.HttpStatusCode.Created, matches);        
            }
            else return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, string.Format("User {0} not found", id));
        }
    }
}