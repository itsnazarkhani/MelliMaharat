-- Students
SELECT
    u.PersonInformation_FirstName,
    u.PersonInformation_LastName, 
    u.Username, 
    u.[Password],
    s.IsDeleted as 'deleted'
FROM Students s 
    INNER JOIN Users u 
        ON s.UserId = u.Id

-- Masters
SELECT 
    m.Id,
    m.Graduation,
    u.PersonInformation_FirstName as 'First Name', 
    u.PersonInformation_LastName as 'Last Name', 
    u.Username, 
    u.[Password], 
    d.Name as Department 
FROM Masters m 
    INNER JOIN Users u 
        ON u.Id = m.UserId 
    INNER JOIN Departments d 
        on  d.Id = m.DepartmentId

-- Lessons
SELECT Lessons.Name FROM Lessons WHERE Lessons.IsDeleted = 0

-- Admin Users
SELECT * FROM Users WHERE Users.Username = 'admin' 

-- Users
SELECT * FROM Users

-- Departments
SELECT * FROM Departments ORDER BY Name

--  Lessons
SELECT * FROM Lessons

-- Presentations
SELECT
    u.Username,
    u.[Password],
    p.Id as 'Presentation-Id',
    u.PersonInformation_FirstName as 'First Name',
    u.PersonInformation_LastName as 'Last Name',
    l.Name as 'Lesson Name',
    p.IsDeleted as 'P-Deleted',
    u.IsDeleted as 'U-Deleted',
    l.IsDeleted as 'L.deleted'
FROM Presentations p
    INNER JOIN Masters m
        ON m.Id = p.MasterId
        INNER JOIN Users u
            ON u.Id = m.UserId
    INNER JOIN Lessons l
        ON l.Id = p.LessonId
WHERE p.IsDeleted = 0 AND u.IsDeleted = 0 AND l.IsDeleted = 0 

-- Selections
SELECT
    u.PersonInformation_FirstName as 'Stu-FirstName',
    u.PersonInformation_LastName as 'Stu-LastName',
    u.Username as 'Stu-Username',
    u.[Password] as 'Stu-Password',
    p.Id as 'Presentation Id'
FROM Selections s
    INNER JOIN Students stu
        ON stu.Id = s.StudentId
            INNER JOIN Users u 
                ON u.Id = stu.UserId
    INNER JOIN Presentations p 
        ON p.Id = s.PresentationId

-- Terms
SELECT * FROM Terms

-- Specific Master Lesson Nmaes
SELECT 
    l.Name 
FROM Lessons l 
    INNER JOIN Presentations p 
        on p.LessonId = l.Id 
    INNER JOIN Masters m 
        on p.MasterId = m.Id 
    INNER JOIN Users u 
        on u.Id = m.UserId 
WHERE m.Id = 'b88fb030-3fb1-4d89-0db0-08de371ef142'