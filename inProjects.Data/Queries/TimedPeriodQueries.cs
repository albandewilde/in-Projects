
using CK.SqlServer;
using CK.SqlServer.Setup;
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

        //public async Task<int> GetLastPeriodBySchool(int schoolId )
        //{
            
        //}

    }
}
