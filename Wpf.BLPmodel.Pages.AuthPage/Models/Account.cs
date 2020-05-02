using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //static void FormingAccessList()
        //{
        //    redact = (from n in Notes.data_Notes where n.writer == NameAccount select n.name).ToList();
        //}
    }
}
