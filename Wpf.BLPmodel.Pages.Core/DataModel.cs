using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace ClientSide
{
    public class Data_Model
    {
        public string type;
    }

    [Serializable]
    class Data_Authentication : Data_Model
    {
        public string login;
        public byte[] password;
        public string operation;//авторизация, регистрация, смена пароля
    }
    [Serializable]
    public class Data_Note : Data_Model
    {
        public string name;//Название
        public string data;//содержание
        public int secflag;//уровень допуска

    }
    [Serializable]
    public class Notes_
    {
        public List<Data_Note> notes_ = new List<Data_Note>();
    }
    static class Serialize
    {

        public static string SerializeAuth(Data_Authentication ob)
        {
            return JsonConvert.SerializeObject(ob);
        }
        public static string SerializeNoteRequest(string options, string account)
        {
            return JsonConvert.SerializeObject(options + account);
        }
        public static string SerializeNote(Data_Note data_Note)
        {

            return JsonConvert.SerializeObject(data_Note);
        }
        public static Notes_ DeserializeNote(string jsonstr)
        {
            return JsonConvert.DeserializeObject<Notes_>(jsonstr);
        }
    }
}
