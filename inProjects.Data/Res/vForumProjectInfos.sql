CREATE VIEW IPR.vForumProjectInfos
AS

SELECT g.GroupName as ProjectName,
       p.ProjectStudentId,
       p.Logo,
       p.Slogan,
       p.Pitch,
       p.[Type],
       p.TraitId,
       g.ZoneId as ForumId,
       fi.ClassRoom,
       fi.CoordinatesX,
       fi.CoordinatesY,
       fi.Width,
       fi.Height
       
FROM IPR.tProjectStudent p
JOIN CK.tGroup g on p.ProjectStudentId = g.GroupId
JOIN IPR.tForumInfos fi on p.ProjectStudentId = fi.ProjectId
