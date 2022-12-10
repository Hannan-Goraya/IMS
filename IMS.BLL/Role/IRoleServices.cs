using IMS.Domain.Users;

namespace IMS.BLL.Role
{
    public interface IRoleServices
    {
        int AddRole(AppRole role);
        int AddUserRole(int UId, int RId);
        List<RoleEdit> GetAllRole(int uId);
        IEnumerable<UserListWithRole> GetAllUsers(UserListWithRole model);
        void RemoveRole(int userId, int roleId);
    }
}