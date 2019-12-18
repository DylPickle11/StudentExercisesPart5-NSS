--UPDATE Student Set CohortId = 1 Where Id =5;

Select s.CohortId, c.[Name], Count(s.CohortId) StudentCount 
From Student s
JOIN Cohort c on s.CohortId = c.id
GROUP BY CohortId, c.[Name];