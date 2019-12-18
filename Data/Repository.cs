using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StudentExercisesPart5.Models;

namespace StudentExercisesPart5
{
    class Repository


    {
        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }
        //Query the database for all Exercises

        public List<Exercise> GetAllExercises()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, Language FROM Exercise";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);
                        int LanguageColumnPosition = reader.GetOrdinal("Language");
                        string LanguageValue = reader.GetString(LanguageColumnPosition);

                        Exercise exercise = new Exercise
                        {
                            //Id = idValue,
                            Name = NameValue,
                            Language = LanguageValue
                        };

                        exercises.Add(exercise);
                    }
                    reader.Close();

                    return exercises;
                }
            }
        }
        public List<Exercise> GetSpecificExercise(string Language)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, Language FROM Exercise WHERE Language = @Language";
                    cmd.Parameters.Add(new SqlParameter("@Language", Language));
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("Name");
                        string NameValue = reader.GetString(NameColumnPosition);
                        int LanguageColumnPosition = reader.GetOrdinal("Language");
                        string LanguageValue = reader.GetString(LanguageColumnPosition);

                        Exercise exercise = new Exercise
                        {
                            //Id = idValue,
                            Name = NameValue,
                            Language = LanguageValue
                        };

                        exercises.Add(exercise);
                    }
                    reader.Close();

                    return exercises;
                }
            }
        }
        // Insert a new exercise into the database
        public Exercise AddExercise(Exercise exercise)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // These SQL parameters are annoying. Why can't we use string interpolation?
                    // ... sql injection attacks!!!
                    cmd.CommandText = "INSERT INTO Exercise (Name, Language) OUTPUT INSERTED.Id Values (@Name, @Language)";
                    cmd.Parameters.Add(new SqlParameter("@Name", exercise.Name));
                    cmd.Parameters.Add(new SqlParameter("@Language", exercise.Language));
                    int id = (int)cmd.ExecuteScalar();

                    exercise.Id = id;

                    return exercise;
                }
            }
        }
        //Find All Instructors with Cohort
        public List<Instructor> GetInstructorWithCohort()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT i.Id, i.FirstName, i.LastName, i.SlackHandle, i.Speciality, c.[Name] FROM Instructor i LEFT JOIN Cohort c on i.CohortId = c.Id";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int NameColumnPosition = reader.GetOrdinal("FirstName");
                        string FirstNameValue = reader.GetString(NameColumnPosition);

                        int LastNameColumnPosition = reader.GetOrdinal("LastName");
                        string LastNameValue = reader.GetString(LastNameColumnPosition);

                        int SlackColumnPosition = reader.GetOrdinal("SlackHandle");
                        string SlackValue = reader.GetString(SlackColumnPosition);

                        int SpecialityColumnPosition = reader.GetOrdinal("Speciality");
                        string SpecialityValue = reader.GetString(SpecialityColumnPosition);

                        int CohortColumnPosition = reader.GetOrdinal("Name");
                        string CohortValue = reader.GetString(CohortColumnPosition);


                        Instructor instructor = new Instructor
                        {
                            FirstName = FirstNameValue,
                            LastName = LastNameValue,
                            SlackHandle = SlackValue,
                            Speciality = SpecialityValue,
                            CohortName = CohortValue

                        };

                        instructors.Add(instructor);
                    }
                    reader.Close();

                    return instructors;
                }
            }
        }
        // Add a new instructor into the database / Assign to Cohort.
        public Instructor AddInstructor(Instructor instructor, int cohortId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // These SQL parameters are annoying. Why can't we use string interpolation?
                    // ... sql injection attacks!!!
                    cmd.CommandText = "INSERT INTO instructor (FirstName, LastName, SlackHandle, Speciality, CohortId) OUTPUT INSERTED.Id Values (@FirstName, @LastName, @SlackHandle, @Speciality, @CohortId)";
                    cmd.Parameters.Add(new SqlParameter("@FirstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@SlackHandle", instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@Speciality", instructor.Speciality));
                    cmd.Parameters.Add(new SqlParameter("@CohortId", cohortId));

                    int id = (int)cmd.ExecuteScalar();
                    instructor.Id = id;

                    return instructor;
                }
            }
        }
        // This needs to update and create a student exercise () Add
        public void AddAssignment(int studentId, int exerciseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Student SET exerciseId = @exerciseId
                                     WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@exerciseId", exerciseId));
                    cmd.Parameters.Add(new SqlParameter("@id", studentId));
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}



