CREATE VIEW IPR.vProjectsDetails
AS

SELECT LeaderId = tu.TimedUserId,
       LeaderFirstName = u.FirstName,
	   LeaderLastName = u.LastName,
       g.GroupName,
       p.ProjectStudentId,
       p.Logo,
       p.Slogan,
       p.Pitch,
       p.[Type],
       p.TraitId,
       g.ZoneId
FROM IPR.tProjectStudent p
JOIN CK.tGroup g on p.ProjectStudentId = g.GroupId
JOIN IPR.tTimedUser tu on tu.TimedUserId = p.LeaderId
JOIN CK.tUser u on u.UserId = tu.UserId

