using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using CK.Auth;
using CK.Core;
using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.Group;
using inProjects.Data.Data.ProjectStudent;
using inProjects.Data.Data.User;
using inProjects.Data.Queries;
using inProjects.Data.Res.Model;
using inProjects.TomlHelpers;
using inProjects.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace inProjects.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController: Controller
    {
        readonly IStObjMap _stObjMap;
        readonly IAuthenticationInfo _authenticationInfo;

        public ProjectController(IStObjMap stObjMap, IAuthenticationInfo authenticationInfo)
        {
            _stObjMap = stObjMap;
            _authenticationInfo = authenticationInfo;
        }


        [HttpPost("submitProject")]
        [AllowAnonymous]
        public async Task<IActionResult> SubmitProject([FromBody] SubmitProjectModel project)
        {
            ProjectStudentTable projectTable = _stObjMap.StObjs.Obtain<ProjectStudentTable>();           
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            CustomGroupTable groupTable = _stObjMap.StObjs.Obtain<CustomGroupTable>();
            ProjectUrlTable projectUrl = _stObjMap.StObjs.Obtain<ProjectUrlTable>();

           return Ok(await TomlHelpers.TomlHelpers.RegisterProject(
                project.Link,
                project.ProjectType,
                projectTable,
                project.UserId,
                db,
                groupTable,
                _stObjMap,
                projectUrl
            ));
        }

        [HttpGet( "getInfosProject" )]
        [AllowAnonymous]
        public async Task<IActionResult> GetInfosProject( int idProject, int idZone )
        {
            ProjectStudentTable projectTable = _stObjMap.StObjs.Obtain<ProjectStudentTable>();
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, db );

                ProjectDetailsData projectDetails = new ProjectDetailsData
                {
                    Project = await projectQueries.GetDetailProject( idProject ),

                    Students = await projectQueries.GetAllUsersOfProject(idProject)
                };

                List<string> listGroups = await projectQueries.GetGroupsOfProject( idProject);

                listGroups = listGroups.FindAll( x => x.StartsWith( "S0" ) || x == "IL" || x == "SR" );
                string groups = listGroups[0];

                if( listGroups.Count >= 2 )
                {
                    for( int i = 1; i < listGroups.Count; i++ )
                    {
                        groups += "-" + listGroups[i];
                    }
                }
               

                projectDetails.Project.Semester = groups;

                return Ok( projectDetails );

            }

        }

        [HttpGet( "verifyProjectFav" )]
        public async Task<IActionResult> VerifyProjectFav( int idProject )
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            var sqlDataBase = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();


            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, sqlDataBase );

                bool exist = await projectQueries.IsProjectFav( idProject, userId );

                return Ok( exist );
            }
        }

        [HttpGet( "favProject" )]
        [AllowAnonymous]
        public async Task<IActionResult> FavProject( int idProject)
        {
            int userId = _authenticationInfo.ActualUser.UserId;
            UserFavProjectTable favTable = _stObjMap.StObjs.Obtain<UserFavProjectTable>();
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();

            using( var ctx = new SqlStandardCallContext() )
            {
               await favTable.FavOrUnfavProject( ctx, userId, idProject );

               return Ok( );

            }

        }

        [HttpGet("getAllProjects")]
        public async Task<IEnumerable<AllProjectInfoData>> GetAllProjects()
        {
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            int userId = _authenticationInfo.ActualUser.UserId;

            using( var ctx = new SqlStandardCallContext() )
            {
                ProjectQueries projectQueries = new ProjectQueries( ctx, db );
                UserQueries userQueries = new UserQueries( ctx, db );
                GroupQueries groupQueries = new GroupQueries( ctx, db );

                IEnumerable<AllProjectInfoData> projectData = await projectQueries.GetAllProject(userId);
                for( var i = 0; i < projectData.Count(); i++ )
                {
                    IEnumerable<UserByProjectData> userByProject = await userQueries.GetUserByProject( projectData.ElementAt( i ).ProjectStudentId );
                    projectData.ElementAt( i ).BegDate = userByProject.ElementAt( 0 ).BegDate;
                    projectData.ElementAt( i ).EndDate = userByProject.ElementAt( 0 ).EndDate;

                    foreach( var e in userByProject )
                    {
                        IEnumerable<GroupData> groupDatas = await groupQueries.GetAllGroupByTimedUser( e.TimedUserId );
                        projectData.ElementAt( i ).FirstName.Add( e.FirstName );
                        projectData.ElementAt( i ).LastName.Add( e.LastName );
                        projectData.ElementAt( i ).TimedUserId.Add( e.TimedUserId );
                        projectData.ElementAt( i ).UserId.Add( e.UserId );
                    }
                }
                return projectData;
            }
        }

        [HttpGet("getProjectSheet")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProjectSheet(int idx)
        {
            SqlDefaultDatabase db = _stObjMap.StObjs.Obtain<SqlDefaultDatabase>();
            int userId = _authenticationInfo.ActualUser.UserId;

            ProjectData pd;
            List<UserData> lst_usr_data;
            List<string> lst_grp;

            using(SqlStandardCallContext ctx = new SqlStandardCallContext())
            {
                ProjectQueries projectQueries = new ProjectQueries(ctx, db);

                pd = await projectQueries.GetDetailProject(idx);
                lst_usr_data = await projectQueries.GetAllUsersOfProject(idx);
                lst_grp = await projectQueries.GetGroupsOfProject(idx);

            }
            
            string name = pd.Name;
            // semesters of the project
            List<int> semesters = new List<int>();
            foreach (string elem in lst_grp) if (elem[0] == 'S') semesters.Add(int.Parse(elem.Substring(1, elem.Length-1)));
            semesters.Sort();
            string semester = String.Join(", ", semesters);

            // sector of the project
            string sector = lst_grp.Contains("SR") && lst_grp.Contains("IL") ? "IL - SR" : lst_grp.Contains("SR") ? "SR" : lst_grp.Contains("IL") ? "IL" : "";


            // download and encode the image in base64
            string logo = Convert.ToBase64String(new WebClient().DownloadData(pd.Logo));

            string slogan = pd.Slogan;
            string pitch = pd.Pitch;

            // check who is the leader
            string[] members = new string[lst_usr_data.Count-1];
            string leader = "";
            foreach(UserData usr in lst_usr_data)
            {
                if (usr.UserId == pd.LeaderId){leader = usr.FirstName + " " + usr.LastName;}
                else {members[Array.IndexOf(members, null)] = usr.FirstName + " " + usr.LastName;}
                
            }
            (string, string[]) team = (leader, members);

            string[] technos = pd.Technologies.ToArray();

            return Ok(new ProjectPiSheet(name, semester, sector, logo, slogan, pitch, team, technos));
        }
    }
}
