using IMS.Domain.Users;

namespace IMS.BLL.Users
{
    public interface IUserServices
    {
        int AddUser(AppUsers users);
        string CreatePasswordHash(string password);
        AppUsers GetUserByEmail(string email);
        AppUsers GetUserById(int Id);
        void UpdateImage(string email, string Image);
        void UpdatePassword(string email, string Password);
        void UpdateToken(string email, string Token);
        bool VirifyPassword(string password, string dbPassword);
        void UpdateStatus(string email, bool IsVerify);
    }
}