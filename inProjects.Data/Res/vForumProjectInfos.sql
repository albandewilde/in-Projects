CREATE VIEW IPR.vForumProjectInfos
AS

SELECT g.GroupName as ProjectName,
       p.ProjectStudentId,
       p.Logo,
       p.Slogan,
       p.Pitch,
       p.[Type],
       p.TraitId,
       g.ZoneId as SchoolId,
       fi.ClassRoom,
       fi.CoordinatesX,
       fi.CoordinatesY
       
FROM IPR.tProjectStudent p
JOIN CK.tGroup g on p.ProjectStudentId = g.GroupId
JOIN IPR.tForumInfos fi on p.ProjectStudentId = fi.ProjectId
