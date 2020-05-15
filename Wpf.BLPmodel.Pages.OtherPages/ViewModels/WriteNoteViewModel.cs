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

namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels
{
    class WriteNoteViewModel : BaseNumericViewModel
    {
        public WriteNoteViewModel()
        {
            AddNoteCommand = new DelegateCommand(AddNote);
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
        void AddNote()
        {
            if (_DataNote != "" && _DataNote != null && _NameNote != "" && _NameNote != null)
            {
                SendNotetoServ();
            }
            else if((_DataNote == "" || _DataNote == null) && (_NameNote == "" || _NameNote == null) )
                MessageBox.Show("Введите название и текст заметки");
            else if(_DataNote == "" || _DataNote == null)
                MessageBox.Show("Введите текст заметки");
            else MessageBox.Show("Введите название заметки");
        }
        virtual public void SendNotetoServ()
        {
            Data_Note note = new Data_Note { data = _DataNote, name = _NameNote, secflag = SecFlagNavigator };
            if (CheckExist(note))
            {
                note.type = "addNote";
                string serdata = Serialize.SerializeNote(note);
                string result = SendData.Send_Data(serdata);
                if (result == "Ok")
                {
                    MessageBox.Show("Заметка добавлена");
                    Mode.flag1 = GetNote.Grid;
                    Navigator.NavigateTo(PageNames.ThreeView);
                }
            }
            else MessageBox.Show("Заметка с таким названием уже существует");
        }
       protected bool CheckExist(Data_Note note)
        {
            note.type = "checkexist";
            string serdata = Serialize.SerializeNote(note);
            string result = SendData.Send_Data(serdata);
            return (result == "Ok");
            
        }
        public ICommand AddNoteCommand { get; set; }
        private string _DataNote;
        private string _NameNote;
        public string DataNote { get { return _DataNote; } set { SetProperty(ref _DataNote, value); } }
        public string NameNote { get { return _NameNote; } set { SetProperty(ref _NameNote, value); } }
    }
}
