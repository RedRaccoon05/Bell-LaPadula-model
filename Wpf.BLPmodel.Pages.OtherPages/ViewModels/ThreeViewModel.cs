using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Pages.Core;

using System.Windows.Input;

using Wpf.BLPmodel.Pages.Core.Extentions;
using Microsoft.Practices.Prism.Commands;

namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels {

    public class ThreeViewModel : MasterNavigationViewModel
    {

        public ThreeViewModel(){
            GoSettingsCommand = new DelegateCommand(GoSettings);
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
            
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            
        }
        public ICommand GoSettingsCommand { get; set; }

        private void GoSettings()
        {
            Navigator.NavigateTo(PageNames.TwoView);
        }
    }
}
