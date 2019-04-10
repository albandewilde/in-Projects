CREATE TABLE IPR.tSchool
(
    SchoolId INT NOT NULL IDENTITY(0,1),
    [Name] NVARCHAR(126) NOT NULL

    CONSTRAINT PK_tSchool_SchoolId PRIMARY KEY(SchoolId),
)

INSERT INTO IPR.tSchool ([Name]) VALUES (N'');
INSERT INTO IPR.tSchool ([Name]) VALUES ('IN''TECH');
INSERT INTO IPR.tSchool ([Name]) VALUES ('ESIEA');
