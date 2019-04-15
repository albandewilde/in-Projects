-- SetupConfig: {}
--
create procedure IPR.sCreateSchool
(
    @Name nvarchar(128),
    @SchoolIdResult int output
)
as
begin
    --[beginsp]
    --<PreCreate />


    insert into IPR.tSchool ([Name]) VALUES (@Name);
    SET @SchoolIdResult = SCOPE_IDENTITY();
    --<PostCreate revert />

    --[endsp]
end

