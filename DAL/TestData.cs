using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL
{
    public class TestData
    {

        public static void Initialize(ApplicationDbContext db)
        {
            db.Database.EnsureCreatedAsync();

            if (db.Courses.Any())
            {
                return;
            }

            var courses = new Course[]
            {
                new Course{CourseName="C# Start", Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."},
                new Course{CourseName="C#/.NET", Description = "Курс по C#/.Net предназначен для тех, кто хочет получить перспективную и высокооплачиваемую работу C# разработчика."},
                new Course{CourseName="UNITY", Description = "Курс по Unity3D предназначен для тех, кто обладает базовыми знаниями программирования на С# и хочет получить перспективную и высокооплачиваемую работу Unity3D-разработчика."}
            };
            foreach (Course s in courses)
            {
                db.Courses.Add(s);
            }
            db.SaveChanges();

            var groups = new Group[]
            {
                new Group{ CourseId = 2, GroupName = "SR-01" },
                new Group{ CourseId = 2, GroupName = "SR-02" },
                new Group{ CourseId = 1, GroupName = "PR-03" },
                new Group{ CourseId = 1, GroupName = "PR-04" },
                new Group{ CourseId = 1, GroupName = "PR-05" },
                new Group{ CourseId = 3, GroupName = "UR-06" }
            };
            foreach (Group s in groups)
            {
                db.Groups.Add(s);
            }
            db.SaveChanges();

            var students = new Student[]
            {
              new Student{LastName="Пестов",FirstName="Игорь",GroupId=1},
              new Student{LastName="Полищук",FirstName="Павел",GroupId=1},
              new Student{LastName="Токар",FirstName="Чеслав",GroupId=1},
              new Student{LastName="Филатов",FirstName="Никита",GroupId=1},
              new Student{LastName="Коцюбинский",FirstName="Добрыня",GroupId=2},
              new Student{LastName="Исаков",FirstName="Болеслав",GroupId=2},
              new Student{LastName="Лыткин",FirstName="Оскар",GroupId=2},
              new Student{LastName="Силин",FirstName="Роман",GroupId=2},
              new Student{LastName="Власов",FirstName="Владлен",GroupId=3},
              new Student{LastName="Горшков",FirstName="Артур",GroupId=3},
              new Student{LastName="Предыбайло",FirstName="Герман",GroupId=3},
              new Student{LastName="Худобяк",FirstName="Сава",GroupId=3},
              new Student{LastName="Бородай",FirstName="Гавриил",GroupId=4},
              new Student{LastName="Кабанов",FirstName="Антон",GroupId=4},
              new Student{LastName="Комаров",FirstName="Леонид",GroupId=4},
              new Student{LastName="Грабчак",FirstName="Игорь",GroupId=4},
              new Student{LastName="Бобылёв",FirstName="Юрий",GroupId=5},
              new Student{LastName="Федосеев",FirstName="Сава",GroupId=5},
              new Student{LastName="Колесник",FirstName="Геннадий",GroupId=5},
              new Student{LastName="Городецкий",FirstName="Никодим",GroupId=5},
              new Student{LastName="Крюков",FirstName="Фёдор",GroupId=6},
              new Student{LastName="Воронцов",FirstName="Лев",GroupId=6},
              new Student{LastName="Кулаков",FirstName="Богдан",GroupId=6},
              new Student{LastName="Доронин",FirstName="Елисей",GroupId=6}
            };
            foreach (Student s in students)
            {
                db.Students.Add(s);
            }
            db.SaveChanges();

            foreach (var entity in db.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }

        }
    }
}
