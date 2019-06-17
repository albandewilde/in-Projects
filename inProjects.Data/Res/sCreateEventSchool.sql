 --SetupConfig: {"Requires": [ "CK.sGroupCreate", "CK.sGroupGroupNameSet" ]}
--

CREATE PROCEDURE sCreateEventSchool
(
    @Name VARCHAR(256),
	@BegDate DATETIME2,
	@EndDate DATETIME2,	
	@SchoolId INT,
	@ActorId INT,
	@EventId INT OUTPUT
)
AS
BEGIN
--[beginsp]
--<PreCreate />

	  exec CK.sGroupCreate @ActorId, @EventId OUTPUT, @SchoolId
	  exec CK.sGroupGroupNameSet @ActorId, @EventId,@Name

	  insert into IPR.tEventSchool VALUES(@EventId, @BegDate,@EndDate)
	  
	
--<PostCreate revert />

--[endsp]

END
