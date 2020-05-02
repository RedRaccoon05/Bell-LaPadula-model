using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Pages.Core;

namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels {
    public class TwoViewModel : BaseNumericViewModel {

        public TwoViewModel() :base(){ }

        public override bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
           
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
           
        }
    }
}
