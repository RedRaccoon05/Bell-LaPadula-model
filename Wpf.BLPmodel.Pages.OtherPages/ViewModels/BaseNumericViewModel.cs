using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.BLPmodel.Pages.Core;
using Wpf.BLPmodel.Pages.Core.Extentions;

namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels {
    public abstract class BaseNumericViewModel : MasterNavigationViewModel {

        public BaseNumericViewModel() {
            GoBackCommand = new DelegateCommand(GoBack);
        }

        public ICommand GoBackCommand { get; set; }

        private void GoBack() {
            Navigator.NavigateTo(PageNames.ThreeView);
        }
    }
}
