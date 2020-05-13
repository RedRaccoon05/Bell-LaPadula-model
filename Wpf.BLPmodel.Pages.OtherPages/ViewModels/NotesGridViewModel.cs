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
    class NoteViewer
    {
        public string name { get; set; }
        public int secflag { get; set; }
    }
    class NotesViewer
    {
        public List<NoteViewer> _notesViewer = new List<NoteViewer>();
        public NotesViewer(Notes_ notes)
        {
            foreach (var item in notes.notes_)
                _notesViewer.Add(new NoteViewer { name = item.name, secflag = item.secflag });
        }
    }
    class NotesGridViewModel : BaseNumericViewModel
    {
        public NotesGridViewModel()
        {
            MouseDoubleClickCommand = new DelegateCommand<object>(NoteReader);
            AddNoteCommand = new DelegateCommand(NoteWriter);
            GetNotestoServ();
            notesViewer = new NotesViewer(notes);
            DataGridSource = notesViewer._notesViewer;
            Navigator.NavigateTo(PageNames.OneView);
        }
        void NoteReader(object ob)
        {

            NoteViewer r = ob as NoteViewer;
            NoteToRead.name = r.name;
            foreach (var note in notes.notes_)
            {
                if(note.name == r.name)
                {
                    NoteToRead.data = note.data;
                    break;
                }
            }
            Mode.flag1 = GetNote.Read;
            Navigator.NavigateTo(PageNames.OneView);
            Navigator.NavigateTo(PageNames.ThreeView);
            
        }
        void NoteWriter()
        {
            Mode.flag1 = GetNote.Write;
            Navigator.NavigateTo(PageNames.OneView);
            Navigator.NavigateTo(PageNames.ThreeView);
        }
        void GetNotestoServ()
        {
            notes = Serialize.DeserializeNote(SendData.Send_Data("GetNotes" + SecFlagNavigator.ToString()));

        }
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public ICommand MouseDoubleClickCommand { get; set; }
        public ICommand AddNoteCommand { get; set; }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        NotesViewer notesViewer;
        Notes_ notes = new Notes_();
        private List<NoteViewer> _DataGridSource;
        public List<NoteViewer> DataGridSource { get { return _DataGridSource; } set { SetProperty(ref _DataGridSource, value); } }
    }
}
