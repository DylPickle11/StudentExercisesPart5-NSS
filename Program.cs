using System;
using StudentExercisesPart5.Models;

namespace StudentExercisesPart5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get all Exercises

            var Repo = new Repository();
            var ExercisesRepo = Repo.GetAllExercises();
            Console.WriteLine("--------------------");
            Console.WriteLine("All Current Exercises");

            foreach (var exercise in ExercisesRepo)
            {
                Console.WriteLine($"{ exercise.Name}, { exercise.Language}");
            }


            //Get all Javascript Exercises
            var SpecificExercisesRepo = Repo.GetSpecificExercise("JavaScript");
            Console.WriteLine("--------------------");
            Console.WriteLine("All JavaScript Exercises");

            foreach (var exercise in SpecificExercisesRepo)
            {
                Console.WriteLine($"{ exercise.Name}, { exercise.Language}");
            }

            // Insert a new exercise into the database
            Exercise exercise6 = new Exercise();
            exercise6.Name = "exercise6";
            exercise6.Language = "SQL";
            Repo.AddExercise(exercise6);

            //Find all instructors in the Database. Include each instructor's cohort
            var InstructorsRepo = Repo.GetInstructorWithCohort();
            Console.WriteLine("--------------------");
            Console.WriteLine("All Instructors with Cohort Exercises");

            foreach (var instructor in InstructorsRepo)
            {
                Console.WriteLine($"{ instructor.FirstName}, { instructor.LastName} SlackHandle:{instructor.SlackHandle} Speciality:{instructor.Speciality} Cohort:{instructor.CohortName}");
            }

            // Insert a new instructor into the database. Include each instructor's cohort.
            Instructor instructor1 = new Instructor();

            instructor1.FirstName = "Calvin";
            instructor1.LastName = "Hobkins";
            instructor1.Speciality = "Pudding Making";
            instructor1.SlackHandle = "CalHobs";
            //Repo.AddInstructor(instructor1, 3);


            // Assign an existing exercise to an existing student

            Repo.AddAssignment(2, 4);






        }
    }
}
