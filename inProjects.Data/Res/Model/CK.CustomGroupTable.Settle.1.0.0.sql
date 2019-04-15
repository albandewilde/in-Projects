DECLARE @GroupIdResult INT;

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
