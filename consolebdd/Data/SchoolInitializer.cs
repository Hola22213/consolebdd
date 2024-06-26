﻿using consolebdd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consolebdd.Data
{
    public static class SchoolInitializer
    {
        public static void Seed(SchoolContext context)
        {
            var departments = new List<Department>
            {
                new Department{Name="Math"},
                new Department{Name="Physics"},
                new Department{Name="Science"}
            };
            departments.ForEach(s => context.Departments.Add(s));
            context.SaveChanges();

            var students = new List<Student>
            {
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{Id=1050,Title="Chemistry",Credits=3,DepartmentId=1},
                new Course{Id=4022,Title="Microeconomics",Credits=3,DepartmentId=1},
                new Course{Id=4041,Title="Macroeconomics",Credits=3,DepartmentId=1},
                new Course{Id=1045,Title="Calculus",Credits=4,DepartmentId=2},
                new Course{Id=3141,Title="Trigonometry",Credits=4,DepartmentId=1},
                new Course{Id=2021,Title="Composition",Credits=3,DepartmentId=3},
                new Course{Id=2042,Title="Literature",Credits=4,DepartmentId=3}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentId=1,CourseId=1050,Grade=Grade.A},
                new Enrollment{StudentId=1,CourseId=4022,Grade=Grade.C},
                new Enrollment{StudentId=1,CourseId=4041,Grade=Grade.B},
                new Enrollment{StudentId=2,CourseId=1045,Grade=Grade.B},
                new Enrollment{StudentId=2,CourseId=3141,Grade=Grade.F},
                new Enrollment{StudentId=2,CourseId=2021,Grade=Grade.F},
                new Enrollment{StudentId=3,CourseId=1050},
                new Enrollment{StudentId=4,CourseId=1050,},
                new Enrollment{StudentId=4,CourseId=4022,Grade=Grade.F},
                new Enrollment{StudentId=5,CourseId=4041,Grade=Grade.C},
                new Enrollment{StudentId=6,CourseId=1045},
                new Enrollment{StudentId=7,CourseId=3141,Grade=Grade.A},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}
