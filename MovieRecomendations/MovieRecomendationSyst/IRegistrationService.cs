using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{
    public interface IRegistrationService
    {
        void AddUser(string email, string name, string surname);
    }
}
