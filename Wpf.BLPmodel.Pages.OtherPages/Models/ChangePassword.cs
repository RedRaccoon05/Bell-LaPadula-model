using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace ClientSide
{
    /// <summary>
    /// Смена пароля пользователя
    /// </summary>
    static class ChangePassword
    {
        static public Data_Authentication data_change_pass = new Data_Authentication();
        static public void ShangePass_(string login_, string new_pass_)
        {
            data_change_pass.type = "auth";
            data_change_pass.login = login_;
            SHA256 hash = SHA256.Create();
            data_change_pass.password = hash.ComputeHash(Encoding.UTF8.GetBytes(new_pass_));
            data_change_pass.operation = "changepassword";
        }
    }
}
