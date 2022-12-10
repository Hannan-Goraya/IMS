using Dapper;

namespace IMS.DAL.Dapper
{
    public interface IDapperRepository
    {
        int CreateUserReturnFKInt(string StoredProcedure, DynamicParameters parameter = null);
        int ReturnInt(string StoredProcedure, DynamicParameters parameter = null);
        int CreateUserReturnRoleInt(string StoredProcedure, DynamicParameters parameter = null);
        void Execute(string procrdureName, DynamicParameters parameter);
        T ExecuteReturnScalar<T>(string procrdureName, DynamicParameters parameter = null);
        IEnumerable<T> ReturnList<T>(string procrdureName, DynamicParameters parameter = null);
    }
}