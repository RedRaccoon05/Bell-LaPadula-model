using System.Collections.Generic;
namespace ClientSide
{
    static class Data_Account
    {
        static string NameAccount;
        static int access_level;
        static List<string> redact;//содержит имена заметок доступных для редакта
        static void Data_Account_Initialization(string login, int access)//логин можно с клиента
        {
            NameAccount = login; access_level = access;
        }
    }
}
