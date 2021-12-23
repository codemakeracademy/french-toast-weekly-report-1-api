using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace CM.WeeklyTeamReport.Domain
{
    [ExcludeFromCodeCoverage]
    public class TeamMemberRepository : IRepository<TeamMember>
    {
        private readonly IConfiguration _configuration;
        public TeamMemberRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TeamMemberRepository()
        {

        }
        SqlConnection GetSqlConnection()
        {
            var connectionString = _configuration.GetConnectionString("AntonK");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public TeamMember Create(TeamMember teamMember)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("INSERT INTO TeamMembers (FirstName,  LastName, Title, Mail, Subject, CompanyId)" +
                                                           "VALUES (@FirstName,  @LastName, @Title, @Mail, @Subject, @CompanyId);" +
                                                           "SELECT * FROM TeamMembers WHERE TeamMemberId = SCOPE_IDENTITY()", connection);
                SqlParameter FirstName = new("@FirstName", SqlDbType.NVarChar, 20)
                {
                    Value = teamMember.FirstName
                };
                SqlParameter LastName = new("@LastName", SqlDbType.NVarChar, 20)
                {
                    Value = teamMember.LastName
                };
                SqlParameter Title = new("@Title", SqlDbType.NVarChar, 100)
                {
                    Value = teamMember.Title
                };
                SqlParameter Subject = new("@Subject", SqlDbType.NVarChar, 600)
                {
                    Value = teamMember.Subject
                };
                SqlParameter Mail = new("@Mail", SqlDbType.NVarChar, 100)
                {
                    Value = teamMember.Mail
                };
                SqlParameter CompanyId = new("@CompanyId", SqlDbType.Int)
                {
                    Value = teamMember.CompanyId
                };

                command.Parameters.AddRange(new object[] { FirstName, LastName, Title, Mail, Subject, CompanyId });
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapTeamMember(reader);
                }
            }
            return null;
        }

        public void Delete(int teamMemberId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("DELETE FROM TeamMembers WHERE TeamMemberId = @TeamMemberId", connection);
                SqlParameter TeamMemberId = new("@TeamMemberId", SqlDbType.Int)
                {
                    Value = teamMemberId
                };

                command.Parameters.Add(TeamMemberId);
                command.ExecuteNonQuery();
            }
        }

        public TeamMember Read(int teamMemberId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM TeamMembers WHERE TeamMemberId = @TeamMemberId", connection);
                SqlParameter TeamMemberId = new("@TeamMemberId", SqlDbType.Int)
                {
                    Value = teamMemberId
                };

                command.Parameters.Add(TeamMemberId);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapTeamMember(reader);
                }
            }
            return null;
        }

        public TeamMember Update(TeamMember teamMember)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("UPDATE TeamMembers " +
                                             "SET FirstName = @FirstName, LastName = @LastName, Title = @Title " +
                                             "WHERE TeamMemberId = @TeamMemberId;" +
                                             "SELECT * FROM TeamMembers WHERE TeamMemberId = @TeamMemberId", connection);
                SqlParameter FirstName = new("@FirstName", SqlDbType.NVarChar, 20)
                {
                    Value = teamMember.FirstName
                };
                SqlParameter LastName = new("@LastName", SqlDbType.NVarChar, 20)
                {
                    Value = teamMember.LastName
                };
                SqlParameter Title = new("@Title", SqlDbType.NVarChar, 100)
                {
                    Value = teamMember.Title
                };
                SqlParameter TeamMemberId = new("@TeamMemberId", SqlDbType.Int)
                {
                    Value = teamMember.TeamMemberId
                };

                command.Parameters.AddRange(new object[] { FirstName, LastName, Title, TeamMemberId });
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapTeamMember(reader);
                }
            }
            return null;
        }

        private static TeamMember MapTeamMember(SqlDataReader reader)
        {
            return new TeamMember()
            {
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Title = reader["Title"].ToString(),
                Mail = reader["Mail"].ToString(),
                Subject = reader["Subject"].ToString(),
                TeamMemberId = (int)reader["TeamMemberId"],
                CompanyId = (int)reader["CompanyId"]
            };
        }

        public List<TeamMember> ReadAll()
        {
            List<TeamMember> teamMembers = new();
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM TeamMembers", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var teamMember = MapTeamMember(reader);
                    teamMembers.Add(teamMember);
                }
                return teamMembers;
            }
        }

        public List<TeamMember> ReadAllById(int companyId)
        {
            List<TeamMember> teamMembers = new();
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM TeamMembers WHERE CompanyId=@CompanyId", connection);

                SqlParameter CompanyId = new("@CompanyId", SqlDbType.Int)
                {
                    Value = companyId
                };

                command.Parameters.Add(CompanyId);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var teamMember = MapTeamMember(reader);
                    teamMembers.Add(teamMember);
                }
                return teamMembers;
            }
        }

        public TeamMember ReadMemberBySub(string subject)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM TeamMembers WHERE Subject=@Subject", connection);

                SqlParameter Subject = new("@Subject", SqlDbType.NVarChar, 600)
                {
                    Value = subject
                };

                command.Parameters.Add(Subject);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return MapTeamMember(reader);
                }
            }
            return null;
        }
    }
}
