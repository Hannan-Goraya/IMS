using Dapper;
using IMS.DAL.Dapper;
using IMS.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BLL.Role
{
    public class RoleServices : IRoleServices
    {
        private readonly IDapperRepository _dapper;

        public RoleServices(IDapperRepository dapper)
        {
            _dapper = dapper;
        }
        public int AddRole(AppRole role)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@RoleId", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@RoleName", role.RoleName);
            return _dapper.CreateUserReturnRoleInt("AddRole", param);
        }


        public int AddUserRole(int UId, int RId)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@UId", UId);
            param.Add("@RId", RId);
            return _dapper.CreateUserReturnFKInt("AddUserRole", param);
        }
        public List<RoleEdit> GetAllRole(int uId)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", uId);
            return _dapper.ReturnList<RoleEdit>("dbo.GetAllRole", param).ToList();

        }

        public void RemoveRole(int userId, int roleId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserId", userId);
            param.Add("@RoleId", roleId);
            _dapper.CreateUserReturnFKInt("DeleteRole", param);
        }
        public IEnumerable<UserListWithRole> GetAllUsers(UserListWithRole model)
        {
            List<UserListWithRole> user = new List<UserListWithRole>();
            user = _dapper.ReturnList<UserListWithRole>("GetUserByRole").ToList();
            return (user);
        }



    }
}
