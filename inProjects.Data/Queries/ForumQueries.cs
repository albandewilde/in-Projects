using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.ForumsPlan;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;

namespace inProjects.Data.Queries
{
    public class ForumQueries
    {
        private ISqlConnectionController _controller;

        public ForumQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );
        }

        public async Task<List<int>> SavePlan( Project[] plan )
        {
            List<int> results = new List<int>();

            for(int i = 0; i < plan.Length; i += 1 )
            {

                int res = await _controller.ExecuteAsync(
                    "update IPR.tForumInfos " +
                    "set ClassRoom = @Class," +
                    "CoordinatesX = @X," +
                    "CoordinatesY = @Y," +
                    "Width = @W," +
                    "Height = @H " +
                    "where ForumNumber = @ForumNumber",
                    new {
                        Class = plan[i].ClassRoom,
                        X = plan[i].PosX,
                        Y = plan[i].PosY,
                        W = plan[i].Width,
                        H = plan[i].Height
                    }
                );
                results.Add( res );
            }
            return results;
        }
    }
}
