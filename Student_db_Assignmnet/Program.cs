using System;
using System.Data.Entity;

// Define the Student class
public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

// Create a DbContext class for the database interaction
public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
}

class Program
{
    static void Main()
    {
        // Call the method to create and seed the database
        InitializeDatabase();

        // Display all students in the database
        DisplayAllStudents();
    }

    static void InitializeDatabase()
    {
        // Create an instance of SchoolContext
        using (var context = new SchoolContext())
        {
            // Ensure the database is created
            context.Database.CreateIfNotExists();

            // Add a new student to the Students DbSet if the table is empty
            if (!context.Students.Any())
            {
                var newStudent = new Student
                {
                    FirstName = "John",
                    LastName = "Doe"
                };
                context.Students.Add(newStudent);

                // Save changes to the database
                context.SaveChanges();

                Console.WriteLine("Database initialized with a student.");
            }
            else
            {
                Console.WriteLine("Database already contains data. No action taken.");
            }
        }
    }

    static void DisplayAllStudents()
    {
        // Retrieve and display all students from the database
        using (var context = new SchoolContext())
        {
            var students = context.Students.ToList();

            Console.WriteLine("List of Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentId}, Name: {student.FirstName} {student.LastName}");
            }
        }
    }
}

