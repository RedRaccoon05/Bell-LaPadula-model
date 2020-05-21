using ClientSide;
using Microsoft.Practices.Prism.Regions;
using System.Windows;
using Wpf.BLPmodel.Pages.Core.Extentions;
using Wpf.BLPmodel.Pages.OtherPages.Views;

namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels
{
    /// <summary>
    /// Редактирование заметок
    /// </summary>
    class EditingNoteViewModel : WriteNoteViewModel
    {
        public EditingNoteViewModel()
        {
            DataNote = NoteToRead.data;//инициализируем заметку данными выбранной заметки
            NameNote = NoteToRead.name;

        }
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        public override void SendNotetoServ()
        {

            Data_Note note = new Data_Note { data = DataNote, name = NameNote, secflag = SecFlagNavigator };
            if (CheckExist(note) || note.name == NoteToRead.name)// Проверка изменения заметки
            {
                note.data = NoteToRead.data; note.name = NoteToRead.name; note.secflag = SecFlagNavigator;
                note.type = "deleteNote";// сначало удалим старую заметку
                string serdata = Serialize.SerializeNote(note);
                string result = SendData.Send_Data(serdata);
                if (result == "Ok")
                {
                    note = new Data_Note { data = DataNote, name = NameNote, secflag = SelectedSecFlag, type = "addNote" };
                    serdata = Serialize.SerializeNote(note);
                    result = SendData.Send_Data(serdata);//потом добавим новую
                    if (result == "Ok")
                    {
                        MessageBox.Show("Заметка отредактирована");
                        Mode.ModeFlag = GetNote.Grid;
                        Navigator.NavigateTo(PageNames.ThreeView);
                    }
                }
            }
            else MessageBox.Show("Заметка с таким названием уже существует");
        }

    }
}
