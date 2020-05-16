using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using ClientSide;
using Wpf.BLPmodel.Pages.Core;
using System.Windows;
using Wpf.BLPmodel.Pages.Core.Extentions;
using Wpf.BLPmodel.Pages.OtherPages.Views;
using System.Text.RegularExpressions;

namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels
{
    class EditingNoteViewModel : WriteNoteViewModel
    {
        public EditingNoteViewModel()
        {
            DataNote = NoteToRead.data;
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
            if (CheckExist(note)|| note.name ==NoteToRead.name)
            {
                note.data = NoteToRead.data; note.name = NoteToRead.name; note.secflag = SecFlagNavigator;
                note.type = "deleteNote";
                string serdata = Serialize.SerializeNote(note);
                string result = SendData.Send_Data(serdata);
                if (result == "Ok")
                {
                    note = new Data_Note { data = DataNote, name = NameNote, secflag = SelectedSecFlag, type = "addNote" };
                    serdata = Serialize.SerializeNote(note);
                    result = SendData.Send_Data(serdata);
                    if (result == "Ok")
                    {
                        MessageBox.Show("Заметка отредактирована");
                        Mode.flag1 = GetNote.Grid;
                        Navigator.NavigateTo(PageNames.ThreeView);
                    }
                }
            }
            else MessageBox.Show("Заметка с таким названием уже существует");
        }

    }
}
