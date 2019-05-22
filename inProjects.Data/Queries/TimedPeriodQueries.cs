
using CK.SqlServer;
using CK.SqlServer.Setup;
using Dapper;
using inProjects.Data.Data.Period;
using System.Threading.Tasks;

namespace inProjects.Data.Queries
{
    public class TimedPeriodQueries
    {
            private ISqlConnectionController _controller;

            public TimedPeriodQueries(ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
            {
                _controller = ctx.GetConnectionController( sqlDefaultDatabase );

            }

        public async Task<PeriodData> GetLastPeriodBySchool( int schoolId )
        {
            PeriodData result = await _controller.QuerySingleOrDefaultAsync<PeriodData>( "SELECT TOP(1) OrderedChild = ChildOrderByKey.ToString(), ChildId, ZoneId FROM CK.vZoneDirectChildren WHERE ZoneId = @ZoneId ORDER BY ChildOrderByKey DESC;", new { ZoneId = schoolId } );
            return result;
        }

        public async Task<PeriodData> GetCurrentPeriod(int zoneId )
        {
            PeriodData result = await _controller.QuerySingleOrDefaultAsync<PeriodData>( "SELECT * FROM IPR.tTimePeriod WHERE TimePeriodId = @ZoneId;", new { ZoneId = zoneId } );
            return result;
        }

    }
}
