CREATE TABLE [IP].tSchool
(
    SchoolId INT NOT NULL IDENTITY(0,1),
    [Name] NVARCHAR(126) NOT NULL

    CONSTRAINT PK_IP_tSchool PRIMARY KEY(SchoolId)
)

INSERT INTO [IP].tSchool ([Name]) VALUES (N'');
INSERT INTO [IP].tSchool ([Name]) VALUES ('IN''TECH');
INSERT INTO [IP].tSchool ([Name]) VALUES ('ESIEA');
