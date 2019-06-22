 --SetupConfig: {}
--

CREATE PROCEDURE sEvaluateProject
(
    @JuryId INT,
    @ProjectId INT,
	@Grade float = null,
	@BegDate DATETIME2 = null,
    @BlockedNote BIT = 0
)
AS
BEGIN
--[beginsp]
--<PreCreate />
	
	if @BegDate is null
		BEGIN
		    UPDATE IPR.tEvaluates set Grade = @Grade where ProjectId = @ProjectId and JuryId = @JuryId
		END
	else
		INSERT INTO IPR.tEvaluates VALUES(@JuryId, @ProjectId, @Grade, @BegDate,@BlockedNote)
	
--<PostCreate revert />

--[endsp]

END
