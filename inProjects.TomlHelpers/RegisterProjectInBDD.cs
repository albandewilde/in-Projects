﻿using CK.SqlServer;
using CK.SqlServer.Setup;
using inProjects.Data;
using inProjects.Data.Data.Group;
using inProjects.Data.Data.Period;
using inProjects.Data.Data.ProjectStudent;
using inProjects.Data.Data.TimedUser;
using inProjects.Data.Data.User;
using inProjects.Data.Queries;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace inProjects.TomlHelpers
{
    public static class RegisterProjectInBDD
    {
        public static async Task SaveProject(
            Type projectType,
            Project toml,
            int userId,
            SqlDefaultDatabase db,
            ProjectStudentTable projectTable,
            CustomGroupTable groupTable
        )
        {

            using(SqlStandardCallContext ctx = new SqlStandardCallContext())
            {
                // letter of project type
                string type;
                if(projectType.Name == "ProjectPi") type = "I";
                else if(projectType.Name == "ProjectPfh") type = "H";
                else type = "¤";

                if(type == "I")
                {
                    ProjectPi project = toml as ProjectPi;

                    // register the project
                    (UserQueries userQueries, TimedUserQueries timedUserQueries, ProjectStudentStruct ProjectCreate, TimedUserData timedUser) = await SaveProjectPi(project, projectTable, userId, db, ctx, type);

                    // link members to the project
                    if (toml.team.members.Length > 0) await LinkMembersToProject(project, userQueries, timedUserQueries, ProjectCreate, timedUser, groupTable, ctx);
                }
            }
        }

        public static async Task<(UserQueries, TimedUserQueries, ProjectStudentStruct, TimedUserData)> SaveProjectPi(
            ProjectPi project,
            ProjectStudentTable projectTable,
            int userId,
            SqlDefaultDatabase db,
            SqlStandardCallContext ctx,
            string type
        )
        {
            GroupQueries groupQueries = new GroupQueries(ctx, db);
            TraitContextQueries traitContext = new TraitContextQueries(ctx, db);
            TimedUserQueries timedUserQueries = new TimedUserQueries(ctx, db);
            TimedPeriodQueries timedPeriodQueries = new TimedPeriodQueries(ctx, db);
            UserQueries userQueries = new UserQueries(ctx, db);

            GroupData school = await groupQueries.GetIdSchoolByConnectUser(userId);
            // PeriodData timePeriod = await timedPeriodQueries.GetLastPeriodBySchool(school.ZoneId);
            TimedUserData timedUser = await timedUserQueries.GetTimedUser(userId, school.ZoneId);
            int traitContextId = await traitContext.GetTraitContextId(type);

            string email = project.team.leader;
            UserData user = await userQueries.GetUserByEmail(email);
            TimedUserData timedLeader = await timedUserQueries.GetTimedUser(user.UserId, school.ZoneId);

            ProjectStudentStruct ProjectCreate = await projectTable.CreateProjectStudent(
                ctx,
                userId,
                school.ZoneId,
                project.name.project_name,
                traitContextId,
                string.Join( ";", project.technologies ),
                project.logo.url,
                project.slogan.slogan,
                project.pitch.pitch,
                timedLeader.TimedUserId,
                timedLeader.UserId,
                type
            );

            return (userQueries, timedUserQueries, ProjectCreate, timedUser);
        }

        public static async Task LinkMembersToProject
        (
            Project project,
            UserQueries userQueries,
            TimedUserQueries timedUserQueries,
            ProjectStudentStruct ProjectCreate,
            TimedUserData timedUser,
            CustomGroupTable groupTable,
            SqlStandardCallContext ctx
        )
        {
            for( int i=0; i < project.team.members.Length; i++ )
            {
                string email = project.team.members[i];
                UserData member = await userQueries.GetUserByEmail(email);
                await groupTable.AddUserAsync(
                    ctx, timedUser.TimedUserId, ProjectCreate.ProjectStudentId, member.UserId, true);
            }
        }
    }
}