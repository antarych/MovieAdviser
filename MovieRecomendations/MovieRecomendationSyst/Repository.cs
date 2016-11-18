using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MovieRecomendationSyst
{
    public class UserProfileRepository:IRepository<UserProfile>
    {
        public UserProfileRepository(string userProfileFile)
        {
            _userProfileFile = userProfileFile;
        }

        public UserProfile[] GetAllEntities()
        {
            string serializedProfiles = File.ReadAllText(_userProfileFile);
            UserProfile[] userProfiles = JsonConvert.DeserializeObject<UserProfile[]>(serializedProfiles);
            return userProfiles;
        }

        public UserProfile GetEntity(int id)
        {
            int ind = -1;
            var allProfiles = GetAllEntities();
            for (int i = 0; i < allProfiles.Length; i++)
            {
                if (allProfiles[i].Id == id)
                {
                    ind = i;
                    break;
                }
            }
            if (ind != -1) { return allProfiles[ind]; }
            else { return null; }
        }

        public void SaveEntity(UserProfile profile)
        {
            var allProfiles = new List<UserProfile>();
            var profiles = GetAllEntities();
            if (profiles != null)
            {
                allProfiles = new List<UserProfile>(GetAllEntities());
            }
            allProfiles.Add(profile);
            File.WriteAllText(_userProfileFile, JsonConvert.SerializeObject(allProfiles.ToArray()));
        }
        private readonly string _userProfileFile;
    }
}
