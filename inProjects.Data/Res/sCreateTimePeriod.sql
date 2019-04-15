-- SetupConfig: {"Requires":["CK.sZoneCreate"]}
--
create procedure IPR.sCreateTimePeriod
(
    @BegDate DATETIME2,
    @EndDate DATETIME2,
    @Kind CHAR,
    @ActorId INT NOT NULL,
    @TimePeriodIdResult INT OUTPUT
)
as
begin
    --[beginsp]
    --<PreCreate />

    EXEC CK.sZoneCreate @ActorId, @TimePeriodResult OUTPUT;
    insert into IPR.tTimePeriod (TimePeriodId, BegDate, EndDate, Kind) VALUES (@TimePeriodResult, @BegDate, @EndDate, @Kind);
    --<PostCreate revert />

    --[endsp]
end

