CREATE TABLE IPR.tProjectStudent
(
    ProjectStudentId INT NOT NULL,
    Logo NVARCHAR(126) NOT NULL,
    Slogan TEXT NOT NULL,
    Pitch NVARCHAR(255) NOT NULL,
    LeaderId INT NOT NULL,
    [Type] CHAR NOT NULL
    


    CONSTRAINT PK_tProjectStudent_ProjectStudentId PRIMARY KEY(ProjectStudentId),
    CONSTRAINT FK_tProjectStudent_ProjectStudentId FOREIGN KEY(ProjectStudentId) REFERENCES CK.tGroup(GroupId),
    CONSTRAINT FK_tProjectStudent_LeaderId FOREIGN KEY(LeaderId) REFERENCES CK.tUser(UserId),
    CONSTRAINT CHK_Type CHECK ([Type] = 'H' OR [Type] ='I')

)