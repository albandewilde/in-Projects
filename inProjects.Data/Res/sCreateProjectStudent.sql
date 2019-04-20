-- SetupConfig: {"Requires": ["CK.sGroupCreate", "CK.sCKTraitFindOrCreate"]}
--
create procedure IPR.sCreateProjectStudent
(
    @ActorId int,
    @ProjectStudentId int output,
    @ZoneId INT,

    @CKTraitContextId INT,
    @FindOnly BIT = 0,
    @TraitName VARCHAR(4096),
    @CKTraitIdResult INT OUTPUT,

    @Logo nvarchar(126),
    @Slogan NVARCHAR(MAX),
    @Pitch NVARCHAR(255),
    @LeaderId INT  =  0,
    @Type CHAR
)
as
begin
    --[beginsp]
    --<PreCreate />
   
    exec CK.sGroupCreate @ActorId, @ProjectStudentId OUTPUT, @ZoneId;
    EXEC CK.sCKTraitFindOrCreate @ActorId, @CKTraitContextId, @FindOnly, @TraitName, @CKTraitIdResult OUTPUT;
    
    insert into IPR.tProjectStudent VALUES (@ProjectStudentId, @Logo, @Slogan, @Pitch, @LeaderId, @Type, @CKTraitIdResult);

    --<PostCreate revert />

    --[endsp]
end

