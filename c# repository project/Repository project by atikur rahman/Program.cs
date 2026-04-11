
using Repository_project_by_atikur_rahman.model_or_entity;
using Repository_project_by_atikur_rahman.repository;
using Repository_project_by_atikur_rahman.service_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_project_by_atikur_rahman
{
    internal class Program
    {
        static void Main(string[] args)
        {

         
            var studentRepo = new GenericRepository<Student>();
            var studentService = new StudentService(studentRepo);

            
            string[] courses = { "C#", "Java", "Python", "SQL", "ASP.NET", "React", "Angular", "Cloud", "Security", "Networking" };

            string[] randomNames = {
                "Atikur Rahman", "Sabbir Ahmed", "Nusrat Jahan", "Tanvir Hossain",
                "Fahmida Akter", "Ariful Islam", "Sadia Afrin", "Rakibul Hasan",
                "Mitu Sarkar", "Kamrul Islam"
            };

            Console.WriteLine("==========insert student===============");

            for (int i = 0; i < 10; i++)
            {
                

                studentService.Register(new Student
                {
                    Id = i + 1,
                    Name = randomNames[i], 
                    EnrolledCourse = new Course { Title = courses[i] }
                   
                });
            }

            Console.WriteLine();

            Console.WriteLine("========Update Student course============");

            studentService.UpdateInfo(new Student
            {
                Id = 1,
                Name = "Atikur Rahman",
                EnrolledCourse = new Course { Title = " MVC.CORE" }
            });
            studentService.UpdateInfo(new Student
            {
                Id = 2,
                Name = "Saikat khan",
                EnrolledCourse = new Course { Title = "LLM" }
            });
            studentService.UpdateInfo(new Student
            {
                Id = 3,
                Name = "Hasibur Rahman",
                EnrolledCourse = new Course { Title = "AI Learning" }
            });
            Console.WriteLine();
            
            Console.WriteLine("=========Delete student=============== ");
            studentService.DeleteStudent(3);
            studentService.DeleteStudent(4);
            studentService.DeleteStudent(5);
            Console.WriteLine();






            Console.WriteLine("=======SHow Student with course=========");
            studentService.ShowAll();

            Console.ReadKey();
        }
    }
}
