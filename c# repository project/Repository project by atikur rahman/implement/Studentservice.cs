using Repository_project_by_atikur_rahman.model_or_entity;
using Repository_project_by_atikur_rahman.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_project_by_atikur_rahman.service_layer
{
    public class StudentService
    {
        private readonly IRepository<Student> _repo;

        public StudentService(IRepository<Student> repo) { _repo = repo; }

        public void Register(Student s)
        {
            _repo.Insert(s);
            Console.WriteLine($"{s.Name.ToLower()} enrolled in {s.EnrolledCourse.Title.ToLower()}");
        }

        public void UpdateInfo(Student s)
        {
            _repo.Update(s);
            Console.WriteLine($"{s.Name.ToLower()} updated successfully");
        }

        public void DeleteStudent(int id)
        {
            var s = _repo.GetById(id);
            if (s != null)
            {
                string name = s.Name;
                _repo.Delete(id);
                Console.WriteLine($"{name.ToLower()} removed successfully");
            }
        }

        public void ShowAll()
        {
            
            foreach (var s in _repo.GetAll())
            {
               
                Console.WriteLine($"id {s.Id} name {s.Name.ToLower()} coursename {s.EnrolledCourse.Title.ToLower()}");
            }
        }
    }
}
