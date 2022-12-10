using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.DAL.Dapper
{
    public class DapperRepository : IDapperRepository
    {
        private readonly string connectionString;
        public DapperRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("default");

        }

        public T ExecuteReturnScalar<T>(string procrdureName, DynamicParameters parameter = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.Execute(procrdureName, parameter, commandType: CommandType.StoredProcedure), typeof(T));
            }

        }
        public void Execute(string procrdureName, DynamicParameters parameter)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procrdureName, parameter, commandType: CommandType.StoredProcedure);
            }

        }



        public IEnumerable<T> ReturnList<T>(string procrdureName, DynamicParameters parameter = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procrdureName, parameter, commandType: CommandType.StoredProcedure);
            }

        }


        public int ReturnInt(string StoredProcedure, DynamicParameters parameter = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, parameter, commandType: CommandType.StoredProcedure);
                return parameter.Get<int>("Id");
            }
        }

        public int CreateUserReturnFKInt(string StoredProcedure, DynamicParameters parameter = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, parameter, commandType: CommandType.StoredProcedure);
                return parameter.Get<int>("UId");
            }
        }

        public int CreateUserReturnRoleInt(string StoredProcedure, DynamicParameters parameter = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(StoredProcedure, parameter, commandType: CommandType.StoredProcedure);
                return parameter.Get<int>("RoleId");
            }
        }


    }
}
