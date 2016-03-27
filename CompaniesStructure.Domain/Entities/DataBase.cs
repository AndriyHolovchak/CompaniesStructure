using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CompaniesStructure.Domain.Entities
{
    public class DataBase
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["CompanyContext"].ConnectionString;

        public List<Company> GetCompanies(int? id)
        {
            var sqlExpression = "GetCompanies";

            var list = new List<Company>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);

                command.CommandType = CommandType.StoredProcedure;

                var parentCompanyId = new SqlParameter
                {
                    ParameterName = "@ParentCompanyId",
                    Value = (object) id ?? DBNull.Value
                };

                command.Parameters.Add(parentCompanyId);


                var reader = command.ExecuteReader();

                int? pcid = null;
                while (reader.Read())
                {
                    
                    if (reader.GetValue(3) != DBNull.Value)
                    {
                        pcid = (int) reader.GetValue(3);
                    }                   

                    list.Add(new Company
                    {
                        Id = (int) reader.GetValue(0),
                        Name = (string) reader.GetValue(1),
                        AnnualEarnings = (decimal) reader.GetValue(2),
                        ParentCompanyId = pcid,
                        TotalCompanyEarnings = Convert.ToDecimal(reader.GetValue(4))
                    });
                }
                reader.Close();
            }

            return list;
        }
    }
}