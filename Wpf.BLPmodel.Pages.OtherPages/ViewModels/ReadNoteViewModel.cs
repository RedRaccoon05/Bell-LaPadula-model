using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Input;
using Wpf.BLPmodel.Pages.Core.Extentions;
using Wpf.BLPmodel.Pages.OtherPages.Views;

namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels
{
    class ReadNoteViewModel : BaseNumericViewModel
    {
        public ReadNoteViewModel()
        {
            EditCommand = new DelegateCommand(Editing);
            NoteContent = NoteToRead.data;
            NoteTitle = NoteToRead.name;
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
        void Editing()
        {
            Mode.ModeFlag = GetNote.Edit;
            Navigator.NavigateTo(PageNames.OneView);
            Navigator.NavigateTo(PageNames.ThreeView);
        }
        public ICommand EditCommand { get; set; }
        private string _NoteContent, _NoteTitle;
        public string NoteContent { get { return _NoteContent; } set { SetProperty(ref _NoteContent, value); } }
        public string NoteTitle { get { return _NoteTitle; } set { SetProperty(ref _NoteTitle, value); } }

    }
}
