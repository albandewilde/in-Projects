CREATE VIEW IPR.vProjectsDetails
AS

SELECT LeaderId = u.UserId,
       LeaderFirstName = u.FirstName,
       LeaderLastName = u.LastName,
       g.GroupName,
       p.ProjectStudentId,
       p.Logo,
       p.Slogan,
       p.Pitch,
       p.[Type],
       p.TraitId,
       g.ZoneId,
       tp.BegDate,
       tp.EndDate
FROM IPR.tProjectStudent p
JOIN CK.tGroup g on p.ProjectStudentId = g.GroupId
JOIN CK.tZone z on g.GroupId = z.ZoneId
JOIN IPR.tTimePeriod tp on tp.TimePeriodId = z.ZoneId
JOIN CK.tUser u on u.UserId = p.LeaderId