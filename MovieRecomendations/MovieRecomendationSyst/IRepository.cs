using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationSyst
{
    public interface IRepository<T>
    {
        T[] GetAllEntities();

        T GetEntity(int id);

        void SaveEntity(T entity);
    }
}
