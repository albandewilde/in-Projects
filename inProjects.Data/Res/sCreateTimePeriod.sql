-- SetupConfig: {"Requires":["CK.sZoneCreate"]}
--
create procedure IPR.sCreateTimePeriod
(
    @ActorId INT,
    @BegDate DATETIME2,
    @EndDate DATETIME2,
    @Kind CHAR,
    @TimePeriodIdResult INT OUTPUT
)
as
begin
    --[beginsp]
    --<PreCreate />

    EXEC CK.sZoneCreate @ActorId, @TimePeriodIdResult OUTPUT;
    insert into IPR.tTimePeriod (TimePeriodId, BegDate, EndDate, Kind) VALUES (@TimePeriodIdResult, @BegDate, @EndDate, @Kind);
    --<PostCreate revert />

    --[endsp]
end

