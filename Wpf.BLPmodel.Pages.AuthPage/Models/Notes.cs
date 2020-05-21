using System.Collections.Generic;

namespace ClientSide
{
    /// <summary>
    /// Инициализация заметок
    /// </summary>
    static class Notes
    {
        static public List<Data_Note> data_Notes = new List<Data_Note>();

        static public void Notes_In(string name_, string date_, string writer_, string notes_, int flag_)
        {
            data_Notes.Add(new Data_Note { type = "notes", name = name_, data = notes_, secflag = flag_ });
        }
    }
}