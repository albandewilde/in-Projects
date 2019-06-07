using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data.ForumsPlan;
using System.Threading.Tasks;
using Dapper;

namespace inProjects.Data.Queries
{
    public class ForumQueries
    {
        private ISqlConnectionController _controller;

        public ForumQueries( ISqlCallContext ctx, SqlDefaultDatabase sqlDefaultDatabase )
        {
            _controller = ctx.GetConnectionController( sqlDefaultDatabase );
        }

        public async Task<int> SavePlan( Project[] plan )
        {
            string sql = "";
            string end = "";

            for(int i = 0; i < plan.Length; i += 1 )
            {
                if(i < plan.Length) end = ", ";
                else end = ";";

                sql += "(" + plan[i].ProjectId + ", "
                + plan[i].ClassRoom + ", "
                + plan[i].PosX + ", "
                + plan[i].PosY + ", "
                + plan[i].Width + ", "
                + plan[i].Height + ", "
                + plan[i].ForumNumber
                + ")" + end;
            }

            int result = await _controller.ExecuteAsync(
                "insert into IPR.tForumInfos(ProjectId, ClassRoom, CoordinatesX, CoordinatesY, Width, Height, ForumNumber) " +
                "values " + sql );
            return result;
        }
    }
}
