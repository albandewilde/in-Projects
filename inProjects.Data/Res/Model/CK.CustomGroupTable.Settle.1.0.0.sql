DECLARE @GroupIdResult INT,
        @SchoolId INT;


EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S1';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S2';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S3';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S4';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S5';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S6';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S7';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S8';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S9';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'S10';


EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'Teacher';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'Administration';

SELECT @SchoolId = SchoolId FROM IPR.tSchool ts WHERE ts.[Name] = 'IN''TECH';


EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT, @SchoolId;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'IL';

EXEC CK.sGroupCreate 1, @GroupIdResult OUTPUT, @SchoolId;
EXEC CK.sGroupGroupNameSet 1, @GroupIdResult, 'SR';
