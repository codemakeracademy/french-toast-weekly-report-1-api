using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CM.WeeklyTeamReport.Domain
{
    [ExcludeFromCodeCoverage]
    public class CompanyRepository : IRepository<Company>
    {
        string connectionString = "Server=ANTON-PC;Database=WeeklyTeamReportLib;Trusted_Connection=True;";
        SqlConnection GetSqlConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public Company Create(Company company)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("INSERT INTO Companies (CompanyName,  JoinDate)" +
                                                           "VALUES (@CompanyName, @JoinDate);" +
                                                           "SELECT * FROM Companies WHERE CompanyId = SCOPE_IDENTITY()", connection);
                SqlParameter CompanyName = new SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 20)
                {
                    Value = company.CompanyName
                };
                SqlParameter JoinDate = new SqlParameter("@JoinDate", System.Data.SqlDbType.Date)
                {
                    Value = company.JoinDate
                };

                command.Parameters.Add(CompanyName);
                command.Parameters.Add(JoinDate);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapCompany(reader);
                }
            }
            return null;
        }

        public void Delete(int companyId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("DELETE FROM Companies WHERE CompanyId = @CompanyId", connection);
                SqlParameter CompanyId = new SqlParameter("@CompanyId", System.Data.SqlDbType.Int)
                {
                    Value = companyId
                };

                command.Parameters.Add(CompanyId);
                command.ExecuteNonQuery();
            }
        }

        public Company Read(int companyId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM Companies WHERE CompanyId = @CompanyId", connection);
                SqlParameter CompanyId = new SqlParameter("@CompanyId", System.Data.SqlDbType.Int)
                {
                    Value = companyId
                };

                command.Parameters.Add(CompanyId);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapCompany(reader);
                }
            }
            return null;
        }

        public Company Update(Company company)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("UPDATE Companies " +
                                             "SET CompanyName = @CompanyName, JoinDate = @JoinDate " +
                                             "WHERE CompanyId = @CompanyId;" +
                                              "SELECT * FROM Companies WHERE CompanyId = @CompanyId", connection);
                SqlParameter CompanyId = new SqlParameter("@CompanyId", System.Data.SqlDbType.Int)
                {
                    Value = company.CompanyId
                };
                SqlParameter CompanyName = new SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 20)
                {
                    Value = company.CompanyName
                };
                SqlParameter JoinDate = new SqlParameter("@JoinDate", System.Data.SqlDbType.Date)
                {
                    Value = company.JoinDate
                };

                command.Parameters.Add(CompanyId);
                command.Parameters.Add(CompanyName);
                command.Parameters.Add(JoinDate);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapCompany(reader);
                }
            }
            return null;
        }


        private static Company MapCompany(SqlDataReader reader)
        {
            return new Company()
            {
                CompanyId = (int)reader["CompanyId"],
                CompanyName = reader["CompanyName"].ToString(),
                JoinDate = reader["JoinDate"].ToString()
            };
        }

        public List<Company> ReadAll()
        {
            List<Company> companies = new List<Company>();
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM Companies", connection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var company = MapCompany(reader);
                    companies.Add(company);
                }
                return companies;
            }
            
        }
    }
}