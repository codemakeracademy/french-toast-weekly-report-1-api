﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace CM.WeeklyTeamReport.Domain
{
    public interface IWeeklyReportRepository<TEntity> : IRepository<WeeklyReport>
    {
        public List<TEntity> GetWeeklyReports(int companyId, int teamMemberId, string dateFrom, string dateTo);
        public List<TEntity> ReadAllById(int teamMemberId);
    }
    [ExcludeFromCodeCoverage]
    public class WeeklyReportRepository : IWeeklyReportRepository<WeeklyReport>
    {
        public IConfiguration _configuration;
        public WeeklyReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WeeklyReportRepository()
        {

        }

        SqlConnection GetSqlConnection()
        {
            var connectionString = _configuration.GetConnectionString("Sql");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public WeeklyReport Create(WeeklyReport weeklyReport)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("INSERT INTO WeeklyReports (DateFrom,  DateTo, MoraleValueId, StressValueId, WorkloadValueId, MoraleComment, StressComment, WorkloadComment, " +
                                             "WeekHighComment, WeekLowComment, AnythingElseComment, TeamMemberId)" +
                                             "VALUES (@DateFrom,  @DateTo, @MoraleValueId, @StressValueId, @WorkloadValueId, @MoraleComment, @StressComment, @WorkloadComment, @WeekHighComment," +
                                             "@WeekLowComment, @AnythingElseComment, @TeamMemberId);" +
                                             "SELECT * FROM WeeklyReports WHERE WeeklyReportId = SCOPE_IDENTITY()", connection);
                SqlParameter DateFrom = new("@DateFrom", SqlDbType.Date)
                {
                    Value = weeklyReport.DateFrom
                };
                SqlParameter DateTo = new("@DateTo", SqlDbType.Date)
                {
                    Value = weeklyReport.DateTo
                };
                SqlParameter MoraleValueId = new("@MoraleValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.MoraleValueId
                };
                SqlParameter StressValueId = new("@StressValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.StressValueId
                };
                SqlParameter WorkloadValueId = new("@WorkloadValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.WorkloadValueId
                };
                SqlParameter MoraleComment = new("@MoraleComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.MoraleComment
                };
                SqlParameter StressComment = new("@StressComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.StressComment
                };
                SqlParameter WorkloadComment = new("@WorkloadComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WorkloadComment
                };
                SqlParameter WeekHighComment = new("@WeekHighComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WeekHighComment
                };
                SqlParameter WeekLowComment = new("@WeekLowComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WeekLowComment
                };
                SqlParameter AnythingElseComment = new("@AnythingElseComment", SqlDbType.NVarChar, 400)
                {
                    Value = weeklyReport.AnythingElseComment
                };
                SqlParameter TeamMemberId = new("@TeamMemberId", SqlDbType.Int)
                {
                    Value = weeklyReport.TeamMemberId
                };

                command.Parameters.AddRange(new object[] { DateFrom, DateTo, MoraleValueId, StressValueId, WorkloadValueId, MoraleComment, StressComment, WorkloadComment, WeekHighComment,
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
                SqlParameter WeeklyReportId = new("@WeeklyReportId", SqlDbType.Int)
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
                SqlParameter WeeklyReportId = new("@WeeklyReportId", SqlDbType.Int)
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
                SqlParameter StartDate = new("@StartDate", SqlDbType.Date)
                {
                    Value = weeklyReport.DateFrom
                };
                SqlParameter EndDate = new("@EndDate", SqlDbType.Date)
                {
                    Value = weeklyReport.DateTo
                };
                SqlParameter MoraleValueId = new("@MoraleValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.MoraleValueId
                };
                SqlParameter StressValueId = new("@StressValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.StressValueId
                };
                SqlParameter WorkloadValueId = new("@WorkloadValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.WorkloadValueId
                };
                SqlParameter MoraleComment = new("@MoraleComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.MoraleComment
                };
                SqlParameter StressComment = new("@StressComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.StressComment
                };
                SqlParameter WorkloadComment = new("@WorkloadComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WorkloadComment
                };
                SqlParameter WeekHighComment = new("@WeekHighComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WeekHighComment
                };
                SqlParameter WeekLowComment = new("@WeekLowComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WeekLowComment
                };
                SqlParameter AnythingElseComment = new("@AnythingElseComment", SqlDbType.NVarChar, 400)
                {
                    Value = weeklyReport.AnythingElseComment
                };
                SqlParameter WeeklyReportId = new("@WeeklyReportId", SqlDbType.Int)
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
                DateFrom = DateTime.Parse(reader["DateFrom"].ToString()).ToString("yyyy-MM-dd"),
                DateTo = DateTime.Parse(reader["DateTo"].ToString()).ToString("yyyy-MM-dd"),
                MoraleValueId = (Morales)(int)reader["MoraleValueId"],
                StressValueId = (Morales)(int)reader["StressValueId"],
                WorkloadValueId = (Morales)(int)reader["WorkloadValueId"],
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
      
        public List<WeeklyReport> GetWeeklyReports(int companyId, int teamMemberId, string dateFrom, string dateTo)
        {
            List<WeeklyReport> existsWeeklyReports = new();
            TeamMemberRepository memberRepository = new(_configuration);
            List<TeamMember> reportFromMembers = new();
            List<WeeklyReport> returnedReports = new();
            using (var connection = GetSqlConnection())
            {
                SqlCommand command;
                SqlParameter DateFrom = new("@DateFrom", SqlDbType.NChar)
                {
                    Value = dateFrom
                };
                SqlParameter DateTo = new("@DateTo", SqlDbType.NChar)
                {
                    Value = dateTo
                };
                if (companyId == 0)
                {
                    command = new SqlCommand("SELECT WR.* " +
                    "FROM TeamMembers TM JOIN ReportFromTo REP ON TM.TeamMemberId = Rep.TeamMemberFrom " +
                    "LEFT JOIN WeeklyReports WR ON TM.TeamMemberId = WR.TeamMemberId " +
                    "WHERE WR.DateFrom = @DateFrom " +
                    "AND WR.DateTo = @DateTo " +
                    "AND Rep.TeamMemberTo = @TeamMemberTo", connection);
                    SqlParameter TeamMemberTo = new("@TeamMemberTo", SqlDbType.Int)
                    {
                        Value = teamMemberId
                    };
                    command.Parameters.AddRange(new object[] { DateFrom, DateTo, TeamMemberTo });
                }
                else
                {
                    command = new SqlCommand("SELECT WR.* " +
                        "FROM TeamMembers TM JOIN WeeklyReports WR on TM.TeamMemberId = WR.TeamMemberId " +
                        "where TM.CompanyId = @CompanyId " +
                        "AND WR.DateFrom = @DateFrom " +
                        "AND WR.DateTo = @DateTo ", connection);
                    SqlParameter CompanyId = new("@CompanyId", SqlDbType.Int)
                    {
                        Value = companyId
                    };
                    command.Parameters.AddRange(new object[] { DateFrom, DateTo, CompanyId });
                }
                
                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var weeklyReport = MapWeeklyReport(reader);
                    existsWeeklyReports.Add(weeklyReport);
                }
                reader.Close();
                command.Parameters.Clear();
                if(companyId == 0)
                {
                    command = new SqlCommand("SELECT TM.* " +
                    "FROM TeamMembers TM JOIN ReportFromTo Rep ON TM.TeamMemberId = Rep.TeamMemberFrom " +
                    "WHERE Rep.TeamMemberTo =  @TeamMemberTo", connection);
                    SqlParameter TeamMemberTo = new("@TeamMemberTo", SqlDbType.Int)
                    {
                        Value = teamMemberId
                    };
                    command.Parameters.Add(TeamMemberTo);
                }
                else
                {
                    command = new SqlCommand("Select * from TeamMembers Where CompanyId = @CompanyId", connection);
                    SqlParameter CompanyId = new("@CompanyId", SqlDbType.Int)
                    {
                        Value = companyId
                    };
                    command.Parameters.Add(CompanyId);
                }
                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    var member = memberRepository.MapTeamMember(reader);
                    reportFromMembers.Add(member);
                }
                foreach (var member in reportFromMembers)
                {
                    bool flag = true;
                    foreach (var report in existsWeeklyReports)
                    {
                        if (report.TeamMemberId == member.TeamMemberId)
                        {
                            flag = false; returnedReports.Add(report); break;
                        }
                    }
                    if (flag) returnedReports.Add(new WeeklyReport());
                    returnedReports[returnedReports.Count - 1].FirstName = member.FirstName;
                    returnedReports[returnedReports.Count - 1].LastName = member.LastName;
                }
                return returnedReports;
            }
        }
        public List<WeeklyReport> ReadAllById(int teamMemberId)
        {
            List<WeeklyReport> weeklyReports = new();
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM WeeklyReports WHERE TeamMemberId=@TeamMemberId " +
                    "order by DateTo desc", connection);

                SqlParameter TeamMemberId = new("@TeamMemberId", SqlDbType.Int)
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
