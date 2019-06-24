-- SetupConfig: {"Requires": ["CK.sGroupCreate", "CK.sCKTraitFindOrCreate", "CK.sGroupUserAdd", "CK.sGroupGroupNameSet"]}
--
create procedure IPR.sCreateProjectStudent
(
    @ActorId int,
    @ProjectStudentId int output,
    @ZoneId INT,
    @Name nvarchar(128),

    @CKTraitContextId INT,
    @FindOnly BIT = 0,
    @TraitName VARCHAR(4096),
    @CKTraitIdResult INT OUTPUT,

    @Logo nvarchar(126),
    @Slogan NVARCHAR(255),
    @Pitch TEXT,
    @LeaderId int = 0,
    @Type CHAR
)
as
begin
    --[beginsp]
    --<PreCreate />
   
    exec CK.sGroupCreate @ActorId, @ProjectStudentId OUTPUT, @ZoneId;
    EXEC CK.sCKTraitFindOrCreate @ActorId, @CKTraitContextId, @FindOnly, @TraitName, @CKTraitIdResult OUTPUT;
    
    insert into IPR.tProjectStudent VALUES (@ProjectStudentId, @Logo, @Slogan, @Pitch, @LeaderId, @Type, @CKTraitIdResult);
    --set @ProjectStudentId = SCOPE_IDENTITY()
    exec CK.sGroupUserAdd @ActorId, @ProjectStudentId, @LeaderId, 1
    exec CK.sGroupGroupNameSet @ActorId, @ProjectStudentId,@Name

    --<PostCreate revert />

    --[endsp]
end

