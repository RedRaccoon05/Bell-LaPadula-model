using System.Security.Cryptography;
using System.Text;
//TODO какие еще могут быть представления данных
namespace ClientSide
{

    static class Authentication
    {
        public static Data_Authentication data_auth = new Data_Authentication();
        public static void Auth_(string login_, string pass_)
        {
            data_auth.type = "auth";
            data_auth.login = login_;
            SHA256 hash = SHA256.Create();
            data_auth.password = hash.ComputeHash(Encoding.UTF8.GetBytes(pass_));
            data_auth.operation = "authentication";
        }

    }

}
