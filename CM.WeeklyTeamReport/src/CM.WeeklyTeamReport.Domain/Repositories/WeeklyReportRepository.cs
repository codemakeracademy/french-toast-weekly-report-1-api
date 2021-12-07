using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CM.WeeklyTeamReport.Domain
{
    [ExcludeFromCodeCoverage]
    public class WeeklyReportRepository : IRepository<WeeklyReport>
    {
        string connectionString = "Server=ANTON-PC;Database=WeeklyTeamReportLib;Trusted_Connection=True;";
        SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public WeeklyReport Create(WeeklyReport weeklyReport)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("INSERT INTO WeeklyReports (StartDate,  EndDate, MoraleValueId, StressValueId, WorkloadValueId, MoraleComment, StressComment, WorkloadComment, " +
                                             "WeekHighComment, WeekLowComment, AnythingElseComment, TeamMemberId)" +
                                             "VALUES (@StartDate,  @EndDate, @MoraleValueId, @StressValueId, @WorkloadValueId, @MoraleComment, @StressComment, @WorkloadComment, @WeekHighComment," +
                                             "@WeekLowComment, @AnythingElseComment, @TeamMemberId);" +
                                             "SELECT * FROM WeeklyReports WHERE WeeklyReportId = SCOPE_IDENTITY()", connection);
                SqlParameter StartDate = new SqlParameter("@StartDate", System.Data.SqlDbType.Date)
                {
                    Value = weeklyReport.StartDate
                };
                SqlParameter EndDate = new SqlParameter("@EndDate", System.Data.SqlDbType.Date)
                {
                    Value = weeklyReport.EndDate
                };
                SqlParameter MoraleValueId = new SqlParameter("@MoraleValueId", System.Data.SqlDbType.Int)
                {
                    Value = (int)weeklyReport.MoraleValue
                };
                SqlParameter StressValueId = new SqlParameter("@StressValueId", System.Data.SqlDbType.Int)
                {
                    Value = (int)weeklyReport.StressValue
                };
                SqlParameter WorkloadValueId = new SqlParameter("@WorkloadValueId", System.Data.SqlDbType.Int)
                {
                    Value = (int)weeklyReport.WorkloadValue
                };
                SqlParameter MoraleComment = new SqlParameter("@MoraleComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.MoraleComment
                };
                SqlParameter StressComment = new SqlParameter("@StressComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.StressComment
                };
                SqlParameter WorkloadComment = new SqlParameter("@WorkloadComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.WorkloadComment
                };
                SqlParameter WeekHighComment = new SqlParameter("@WeekHighComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.WeekHighComment
                };
                SqlParameter WeekLowComment = new SqlParameter("@WeekLowComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.WeekLowComment
                };
                SqlParameter AnythingElseComment = new SqlParameter("@AnythingElseComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.AnythingElseComment
                };
                SqlParameter TeamMemberId = new SqlParameter("@TeamMemberId", System.Data.SqlDbType.Int)
                {
                    Value = weeklyReport.TeamMemberId
                };

                command.Parameters.AddRange(new object[] { StartDate, EndDate, MoraleValueId, StressValueId, WorkloadValueId, MoraleComment, StressComment, WorkloadComment, WeekHighComment,
                                                           WeekLowComment, AnythingElseComment, TeamMemberId });
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapWeeklyReport(reader);
                }
            }
            return null;
        }

        public void Delete(int weeklyReportId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("DELETE FROM WeeklyReports WHERE WeeklyReportId = @WeeklyReportId", connection);
                SqlParameter WeeklyReportId = new SqlParameter("@WeeklyReportId", System.Data.SqlDbType.Int)
                {
                    Value = weeklyReportId
                };

                command.Parameters.Add(WeeklyReportId);
                command.ExecuteNonQuery();
            }
        }

        public WeeklyReport Read(int weeklyReportId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM WeeklyReports WHERE WeeklyReportId = @WeeklyReportId", connection);
                SqlParameter WeeklyReportId = new SqlParameter("@WeeklyReportId", System.Data.SqlDbType.Int)
                {
                    Value = weeklyReportId
                };

                command.Parameters.Add(WeeklyReportId);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapWeeklyReport(reader);
                }
            }
            return null;
        }

        public WeeklyReport Update(WeeklyReport weeklyReport)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("UPDATE WeeklyReports " +
                                             "SET StartDate = @StartDate, EndDate = @EndDate, MoraleValueId = @MoraleValueId, StressValueId = @StressValueId, WorkloadValueId = @WorkloadValueId," +
                                             "MoraleComment = @MoraleComment, StressComment = @StressComment, WorkloadComment = @WorkloadComment, WeekHighComment = @WeekHighComment, WeekLowComment = @WeekLowComment, AnythingElseComment = @AnythingElseComment " +
                                             "WHERE WeeklyReportId = @WeeklyReportId;" +
                                             "SELECT * FROM WeeklyReports WHERE WeeklyReportId = @WeeklyReportId", connection);
                SqlParameter StartDate = new SqlParameter("@StartDate", System.Data.SqlDbType.Date)
                {
                    Value = weeklyReport.StartDate
                };
                SqlParameter EndDate = new SqlParameter("@EndDate", System.Data.SqlDbType.Date)
                {
                    Value = weeklyReport.EndDate
                };
                SqlParameter MoraleValueId = new SqlParameter("@MoraleValueId", System.Data.SqlDbType.Int)
                {
                    Value = (int)weeklyReport.MoraleValue
                };
                SqlParameter StressValueId = new SqlParameter("@StressValueId", System.Data.SqlDbType.Int)
                {
                    Value = (int)weeklyReport.StressValue
                };
                SqlParameter WorkloadValueId = new SqlParameter("@WorkloadValueId", System.Data.SqlDbType.Int)
                {
                    Value = (int)weeklyReport.WorkloadValue
                };
                SqlParameter MoraleComment = new SqlParameter("@MoraleComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.MoraleComment
                };
                SqlParameter StressComment = new SqlParameter("@StressComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.StressComment
                };
                SqlParameter WorkloadComment = new SqlParameter("@WorkloadComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.WorkloadComment
                };
                SqlParameter WeekHighComment = new SqlParameter("@WeekHighComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.WeekHighComment
                };
                SqlParameter WeekLowComment = new SqlParameter("@WeekLowComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.WeekLowComment
                };
                SqlParameter AnythingElseComment = new SqlParameter("@AnythingElseComment", System.Data.SqlDbType.NVarChar)
                {
                    Value = weeklyReport.AnythingElseComment
                };
                SqlParameter WeeklyReportId = new SqlParameter("@WeeklyReportId", System.Data.SqlDbType.Int)
                {
                    Value = weeklyReport.WeeklyReportId
                };

                command.Parameters.AddRange(new object[] { StartDate, EndDate, MoraleValueId, StressValueId, WorkloadValueId, MoraleComment, StressComment, WorkloadComment, WeekHighComment,
                                                           WeekLowComment, AnythingElseComment, WeeklyReportId });
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapWeeklyReport(reader);
                }
            }
            return null;
        }


        private static WeeklyReport MapWeeklyReport(SqlDataReader reader)
        {
            return new WeeklyReport()
            {
                StartDate = reader["StartDate"].ToString(),
                EndDate = reader["EndDate"].ToString(),
                MoraleValue = (Morales)(int)reader["MoraleValueId"],
                StressValue = (Morales)(int)reader["StressValueId"],
                WorkloadValue = (Morales)(int)reader["WorkloadValueId"],
                MoraleComment = reader["MoraleComment"].ToString(),
                StressComment = reader["StressComment"].ToString(),
                WorkloadComment = reader["WorkloadComment"].ToString(),
                WeekHighComment = reader["WeekHighComment"].ToString(),
                WeekLowComment = reader["WeekLowComment"].ToString(),
                AnythingElseComment = reader["AnythingElseComment"].ToString(),
                TeamMemberId = (int)reader["TeamMemberId"],
                WeeklyReportId = (int)reader["WeeklyReportId"]
            };
        }

        public List<WeeklyReport> ReadAll()
        {
            throw new NotImplementedException();
        }

        public List<WeeklyReport> ReadAllById(int teamMemberId)
        {
            List<WeeklyReport> weeklyReports = new List<WeeklyReport>();
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM WeeklyReports WHERE TeamMemberId=@TeamMemberId", connection);

                SqlParameter TeamMemberId = new SqlParameter("@TeamMemberId", System.Data.SqlDbType.Int)
                {
                    Value = teamMemberId
                };

                command.Parameters.Add(TeamMemberId);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var weeklyReport = MapWeeklyReport(reader);
                    weeklyReports.Add(weeklyReport);
                }
                return weeklyReports;
            }
        }
    }
}