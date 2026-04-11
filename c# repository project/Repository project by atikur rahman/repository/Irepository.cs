using Repository_project_by_atikur_rahman.model_or_entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_project_by_atikur_rahman.repository
{
    public interface IRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
