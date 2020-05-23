using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace ClientSide
{
    /// <summary>
    /// Ркгистрация пользователя
    /// </summary>
    static class Registration
    {
        static public Data_Authentication data_reg = new Data_Authentication();
        static public void Reg_(string login_, string pass_)
        {
            data_reg.type = "auth";
            data_reg.login = login_;
            SHA256 hash = SHA256.Create();
            data_reg.password = hash.ComputeHash(Encoding.UTF8.GetBytes(pass_));
            pass_ = string.Empty;
            data_reg.operation = "registration";

        }
        static public bool Check_Pass(string pass)//проверка пароля на требования(хотя бы одна цифра или спец. символ)
        {
            char[] spec_symbol = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '!', '&', '?' };
            bool result_flag = false;
            if (pass.Length <= 8)
                return result_flag;

            foreach (var symbol in spec_symbol)
                if (pass.Contains(symbol))
                {
                    result_flag = true;
                    break;
                }
            pass = string.Empty;
            return result_flag;

        }

    }

}
