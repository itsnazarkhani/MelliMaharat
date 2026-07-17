
select Students.Id, Students.FullName, avg(cast(Selections.Score as float)) as average

from Selections 
inner join Students
on Students.Id = Selections.StudentId
group by Students.Id, Students.FullName
order by average DESC