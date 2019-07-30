using Dapper;
using GroupMeetingASP.NETCoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GroupMeetingASP.NETCoreWebApp
{
    public class GroupGreetingDbModule
    {
        static string strConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectMeeting;Integrated Security=True;";

        public IEnumerable<GroupMeeting> GetGroupMeetings()
        {
            List<GroupMeeting> groupMeetingsList = new List<GroupMeeting>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                groupMeetingsList = con.Query<GroupMeeting>("GetGroupMeetingDetails").ToList();
            }

            return groupMeetingsList;
        }

        public GroupMeeting GetGroupMeetingById(int? id)
        {
            GroupMeeting groupMeeting = new GroupMeeting();
            if (id == null)
                return groupMeeting;

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", id);
                groupMeeting = con.Query<GroupMeeting>("GetGroupMeetingByID", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return groupMeeting;
        }

        public int AddGroupMeeting(GroupMeeting groupMeeting)
        {
            int rowAffected = 0;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProjectName", groupMeeting.ProjectName);
                parameters.Add("@GroupMeetingLeadName", groupMeeting.GroupMeetingLeadName);
                parameters.Add("@TeamLeadName", groupMeeting.TeamLeadName);
                parameters.Add("@Description", groupMeeting.Description);
                parameters.Add("@GroupMeetingDate", groupMeeting.GroupMeetingDate);

                rowAffected = con.Execute("InsertGroupMeeting", parameters, commandType: CommandType.StoredProcedure);
            }

            return rowAffected;
        }

        public int UpdateGroupMeeting(GroupMeeting groupMeeting)
        {
            int rowAffected = 0;

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", groupMeeting.Id);
                parameters.Add("@ProjectName", groupMeeting.ProjectName);
                parameters.Add("@GroupMeetingLeadName", groupMeeting.GroupMeetingLeadName);
                parameters.Add("@TeamLeadName", groupMeeting.TeamLeadName);
                parameters.Add("@Description", groupMeeting.Description);
                parameters.Add("@GroupMeetingDate", groupMeeting.GroupMeetingDate);
                rowAffected = con.Execute("UpdateGroupMeeting", parameters, commandType: CommandType.StoredProcedure);
            }

            return rowAffected;
        }

        public int DeleteGroupMeeting(int id)
        {
            int rowAffected = 0;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                rowAffected = con.Execute("DeleteGroupMeeting", parameters, commandType: CommandType.StoredProcedure);

            }

            return rowAffected;
        }
    }
}
