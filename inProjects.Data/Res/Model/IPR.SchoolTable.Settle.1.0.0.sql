DECLARE @SchoolId INT;

    EXEC CK.sZoneCreate 1, @SchoolId OUTPUT;
    INSERT INTO IPR.tSchool(SchoolId, [Name]) VALUES (@SchoolId, 'Unknown');

    EXEC CK.sZoneCreate 1, @SchoolId OUTPUT;
    INSERT INTO IPR.tSchool(SchoolId, [Name]) VALUES (@SchoolId, 'ESIEA');

    EXEC CK.sZoneCreate 1, @SchoolId OUTPUT;
    INSERT INTO IPR.tSchool(SchoolId, [Name]) VALUES (@SchoolId, 'IN''TECH');
