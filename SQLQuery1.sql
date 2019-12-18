--SELECT
--s.FirstName As StudentFirstName,
--s.LastName As StudentLastName,
--s.SlackHandle As StudentSlackHandle,
--s.CohortId AS StudentCohortId,
--c.[Name],
--i.FirstName AS InstructorFirstName,
--i.LastName AS  InstructorLastName,
--i.SlackHandle As InstructorSlackHandle,
--i.CohortId As InstructorCohortId

--FROM Student s
--LEFT JOIN Cohort c ON  s.CohortId = c.Id
--LEFT JOIN Instructor i ON i.CohortId = c.Id;

--SELECT c.[Name], c.Id, COUNT(i.Id) as InstructorCount
--FROM Cohort c
--LEFT JOIN Instructor i on i.CohortID = c.Id
--GROUP BY c.[Name], c.Id
--HAVING COUNT(i.id) >1;
----Cannot do a where by group by but can do having

SELECT e.Id, e[Name], e.[Language], COUNT(se.StudentId) as StudentAssigned
From Exercise e
JOIN StudentExercise se ON e.Id = se.ExerciseId
GROUP BY  e.Id, e.[Name], e.[Language];
