using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System.Windows.Input;
using Wpf.BLPmodel.Pages.Core;
using Wpf.BLPmodel.Pages.Core.Extentions;
using Wpf.BLPmodel.Pages.OtherPages.Views;

namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels
{

    public class ThreeViewModel : MasterNavigationViewModel
    {

        public ThreeViewModel()
        {
            GoSettingsCommand = new DelegateCommand(GoSettings);
            GoBackNotesCommand = new DelegateCommand(GoBackNotes);
            GoExitCommand = new DelegateCommand(GoExit);
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
            GetSecFlag(); //Получения флага уровня доступа для отобрашение на странице
        }
        public ICommand GoSettingsCommand { get; set; }
        public ICommand GoBackNotesCommand { get; set; }
        public ICommand GoExitCommand { get; set; }
        private int _SecFlag;
        public int SecFlag { get { return _SecFlag; } set { SetProperty(ref _SecFlag, value); } }
        private void GetSecFlag()
        {
            _SecFlag = SecFlagNavigator;
        }
        private void GoSettings()
        {
            Navigator.NavigateTo(PageNames.TwoView);
        }

        private void GoBackNotes()
        {
            Mode.ModeFlag = GetNote.Grid;
            Navigator.NavigateTo(PageNames.ThreeView);
        }

        private void GoExit() //Выход на страницу входа
        {
            Mode.ModeFlag = GetNote.Grid;
            Navigator.NavigateTo(PageNames.AuthView);
        }
    }
}
