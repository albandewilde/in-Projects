CREATE TABLE IPR.tProjectUseTechno
(
    TechnoId INT NOT NULL,
    ProjectId INT NOT NULL

    CONSTRAINT PK_tProjectUseTechno_ProjectId PRIMARY KEY (ProjectId, TechnoId),
    CONSTRAINT FK_tProjectUseTechno_TechnoId FOREIGN KEY (TechnoId) REFERENCES IPR.tTechno(TechnoId),
    CONSTRAINT FK_tProjectUseTechno_ProjectId FOREIGN KEY (ProjectId) REFERENCES IPR.tProjectStudent(ProjectStudentId)

)
