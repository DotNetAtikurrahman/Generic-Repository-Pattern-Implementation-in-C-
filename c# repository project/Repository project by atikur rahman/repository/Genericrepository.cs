using Repository_project_by_atikur_rahman.model_or_entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace Repository_project_by_atikur_rahman.repository
    {
        public class GenericRepository<T> : IRepository<T> where T : BaseModel
        {
            private readonly List<T> _data = new List<T>();

            public IEnumerable<T> GetAll() => _data;

            public T GetById(int id) => _data.FirstOrDefault(x => x.Id == id);

            public void Insert(T entity) => _data.Add(entity);

            public void Update(T entity)
            {
                var existing = GetById(entity.Id);
                if (existing != null)
                {
                    var index = _data.IndexOf(existing);
                    _data[index] = entity;
                }
            }
            public void Delete(int id)
            {
                var existing = GetById(id);
                if (existing != null) _data.Remove(existing);
            }
        }
    }



