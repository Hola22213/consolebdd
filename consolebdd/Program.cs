using consolebdd.Data;
using consolebdd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace consolebdd.App
{
    public class Program
    {
        public static IConfigurationRoot? Configuration { get; set; }

        public static void Main(string[] args)
        {
            ReadConfiguration();

            using (var db = new SchoolContext())
            {
                Console.WriteLine("Creating database...\n");

                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                Console.WriteLine("Seeding database...\n");

                //SchoolInitializer.Seed(db);

                Console.WriteLine("End seeding database...\n");

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Menu:");
                    Console.WriteLine("1. Add a new student");
                    Console.WriteLine("2. Select a course and enroll a student");
                    Console.WriteLine("3. Add a new student with enrollment date");
                    Console.WriteLine("0. Exit");

                    Console.Write("\nEnter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddNewStudent(db);
                            break;
                        case "2":
                            EnrollStudentInCourse(db);
                            break;
                        case "3":
                            AddNewStudentWithEnrollmentDate(db);
                            break;
                        case "0":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
        }

        private static void AddNewStudent(SchoolContext db)
        {
            Console.WriteLine("\nAdding a new student...");

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();

            var newStudent = new Student
            {
                FirstMidName = firstName,
                LastName = lastName,
                EnrollmentDate = DateTime.Now
            };
            db.Students.Add(newStudent);
            db.SaveChanges();

            Console.WriteLine($"\nNew student added successfully:\nFirst Name: {newStudent.FirstMidName}\nLast Name: {newStudent.LastName}\nEnrollment Date: {newStudent.EnrollmentDate}");
        }

        private static void AddNewStudentWithEnrollmentDate(SchoolContext db)
        {
            Console.WriteLine("\nAdding a new student with enrollment date...");

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine();

            int year, month, day;
            while (true)
            {
                Console.Write("Enter enrollment year (e.g., 2023): ");
                if (int.TryParse(Console.ReadLine(), out year) && year > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid year. Please enter a valid year.");
                }
            }

            while (true)
            {
                Console.Write("Enter enrollment month (1-12): ");
                if (int.TryParse(Console.ReadLine(), out month) && month >= 1 && month <= 12)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid month. Please enter a valid month (1-12).");
                }
            }

            while (true)
            {
                Console.Write("Enter enrollment day (1-31): ");
                if (int.TryParse(Console.ReadLine(), out day) && day >= 1 && day <= 31)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid day. Please enter a valid day (1-31).");
                }
            }

            DateTime enrollmentDate = new DateTime(year, month, day);

            var newStudent = new Student
            {
                FirstMidName = firstName,
                LastName = lastName,
                EnrollmentDate = enrollmentDate
            };
            db.Students.Add(newStudent);
            db.SaveChanges();

            Console.WriteLine($"\nNew student added successfully:\nFirst Name: {newStudent.FirstMidName}\nLast Name: {newStudent.LastName}\nEnrollment Date: {newStudent.EnrollmentDate.ToString("yyyy/MM/dd")}");
        }

        private static void EnrollStudentInCourse(SchoolContext db)
        {
            Console.WriteLine("\nEnrolling a student in a course...");

            Console.WriteLine("Available courses:");
            var courses = db.Courses.ToList();
            foreach (var course in courses)
            {
                Console.WriteLine($"ID: {course.Id}, Title: {course.Title}, Credits: {course.Credits}");
            }

            while (true)
            {
                Console.Write("\nEnter the ID of the course you want to enroll the student in (0 to return to menu): ");
                if (int.TryParse(Console.ReadLine(), out int courseId))
                {
                    if (courseId == 0)
                    {
                        return;
                    }

                    var course = db.Courses.Find(courseId);
                    if (course != null)
                    {
                        Console.WriteLine("Course ID added successfully.");

                        Console.Write("Enter student's ID (0 to return to menu): ");
                        if (int.TryParse(Console.ReadLine(), out int studentId))
                        {
                            if (studentId == 0)
                            {
                                return;
                            }

                            var student = db.Students.Find(studentId);
                            if (student == null)
                            {
                                Console.WriteLine("Student not found.");
                            }
                            else
                            {
                                var newEnrollment = new Enrollment
                                {
                                    StudentId = studentId,
                                    CourseId = courseId,
                                    Grade = Grade.A
                                };
                                db.Enrollments.Add(newEnrollment);
                                db.SaveChanges();

                                Console.WriteLine($"\nStudent {student.FirstMidName} {student.LastName} successfully enrolled in course:\nCourse ID: {course.Id}\nCourse Title: {course.Title}\nCredits: {course.Credits}");
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid student ID. Please enter a valid ID.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Id added with succesfuly.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid course ID. Please enter a valid ID.");
                }
            }
        }

        private static void ReadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            ConnectionStrings.DefaultConnection = Configuration["DefaultConnection"];

            Console.WriteLine("Configuration\n");
            Console.WriteLine($@"connectionString (defaultConnection) = ""{ConnectionStrings.DefaultConnection}""");
            Console.WriteLine();
        }
    }
}
