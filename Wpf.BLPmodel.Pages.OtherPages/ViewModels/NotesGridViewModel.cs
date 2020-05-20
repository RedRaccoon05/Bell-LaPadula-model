using ClientSide;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
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

            NoteViewer Notes = ob as NoteViewer;
            NoteToRead.name = Notes.name;
            foreach (var note in notes.notes_)
            {
                if (note.name == Notes.name)
                {
                    string replaceable = @"\\n";
                    string replacement = "\n";
                    Regex regex = new Regex(replaceable);
                    NoteToRead.data = regex.Replace(note.data, replacement);
                    break;
                }
            }
            Mode.ModeFlag = GetNote.Read;
            Navigator.NavigateTo(PageNames.OneView);
            Navigator.NavigateTo(PageNames.ThreeView);

        }
        void NoteWriter()
        {
            Mode.ModeFlag = GetNote.Write;
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

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        public ICommand MouseDoubleClickCommand { get; set; }
        public ICommand AddNoteCommand { get; set; }

        NotesViewer notesViewer;
        Notes_ notes = new Notes_();
        private List<NoteViewer> _DataGridSource;
        public List<NoteViewer> DataGridSource { get { return _DataGridSource; } set { SetProperty(ref _DataGridSource, value); } }
    }
}
