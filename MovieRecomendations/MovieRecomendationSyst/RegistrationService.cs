using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieRecomendationSyst
{
    public class RegistrationService : IRegistrationService
    {
        public RegistrationService(IRepository<UserProfile> userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        private int GetNewId()
        {
            var allProfiles = _userProfileRepository.GetAllEntities();
            var maxId = 1;
            if (allProfiles != null)
            {
                foreach (UserProfile profile in allProfiles)
                {
                    if (profile.Id > maxId)
                        maxId = profile.Id;
                }
                return maxId + 1;
            }
            else return maxId;
        }

        public void AddUser(string email, string name, string surname)
        {
            var id = GetNewId();
            var newUser = new UserProfile(id, email, name, surname, new WatchedMovie[0]);
            _userProfileRepository.SaveEntity(newUser);
        }
        private readonly IRepository<UserProfile> _userProfileRepository;
    }
}
