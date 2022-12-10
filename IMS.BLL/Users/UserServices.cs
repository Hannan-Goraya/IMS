using Dapper;
using IMS.DAL.Dapper;
using IMS.Domain.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BLL.Users
{
    public class UserServices : IUserServices
    {
        private readonly IDapperRepository _dapper;
        public UserServices(IDapperRepository dapper)
        {
            _dapper = dapper;
        }


        public int AddUser(AppUsers users)
        {
            Dapper.DynamicParameters param = new DynamicParameters();
            param.Add("@Id", -1, dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@Name", users.Name);
            param.Add("@Email", users.Email);
            param.Add("@Password", users.Password);
            param.Add("@Image", users.Image);
            param.Add("@Token", users.Token);
            param.Add("@IsVerify", users.IsVerify);
            param.Add("@CreatedDate", users.CreateDate);
            var result = _dapper.ReturnInt("AddUser", param);
            if (result > 0)
            {

            }

            return result;
        }



        public AppUsers GetUserByEmail(string email)
        {



            Dapper.DynamicParameters parameter = new DynamicParameters();


            parameter.Add("@Email", email);




            return _dapper.ReturnList<AppUsers>("GetUserByEmail", parameter).FirstOrDefault();


        }


        public AppUsers GetUserById(int Id)
        {



            DynamicParameters parameter = new DynamicParameters();


            parameter.Add("@Id", Id);




            return _dapper.ReturnList<AppUsers>("GetUserById", parameter).FirstOrDefault();


        }






        public void UpdatePassword(string email, string Password)
        {

            DynamicParameters parameter = new DynamicParameters();


            parameter.Add("@Email", email);
            parameter.Add("@Password", Password);


            _dapper.Execute("", parameter);

        }




        public void UpdateToken(string email, string Token)
        {



            DynamicParameters parameter = new DynamicParameters();


            parameter.Add("@Email", email);
            parameter.Add("@Token", Token);


            _dapper.Execute("ResetToken", parameter);

        }






        public void UpdateStatus(string email, bool IsVerify)
        {
            DynamicParameters parameter = new DynamicParameters();


            parameter.Add("@Email", email);
            parameter.Add("@IsVerify", IsVerify);


            _dapper.Execute("UpdateImage", parameter);
        }






        public void UpdateImage(string email, string Image)
        {
            DynamicParameters parameter = new DynamicParameters();


            parameter.Add("@Email", email);
            parameter.Add("@Image", Image);


            _dapper.Execute("UpdateImage", parameter);
        }






        public string CreatePasswordHash(string password)
        {


            using var hmac = new HMACSHA512();

            byte[] passwordSalt = hmac.Key;
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            string Passalt = Convert.ToBase64String(passwordSalt);
            string Pashash = Convert.ToBase64String(passwordHash);


            var newpassword = Passalt + ":" + Pashash;

            return newpassword;

        }

        public bool VirifyPassword(string password, string dbPassword)
        {
            string[] passwordarry = dbPassword.Split(':');
            byte[] orignalhash = Convert.FromBase64String(passwordarry[0]);
            using (var hmac = new HMACSHA512(orignalhash))
            {
                var verifyHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                var orignalsalt = Convert.FromBase64String(passwordarry[1]);
                return verifyHash.SequenceEqual(orignalsalt);
            }
        }
    }
}
