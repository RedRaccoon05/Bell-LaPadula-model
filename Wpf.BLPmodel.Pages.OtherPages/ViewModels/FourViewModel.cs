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
namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels
{
    class FourViewModel : BaseNumericViewModel
    {
        public FourViewModel()
        {
            testCommand = new DelegateCommand(Gotest);
            testLabel = "Test";
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
        public ICommand testCommand { get; set; }
        private string _testLabel;
        public string testLabel { get { return _testLabel; } set { SetProperty(ref _testLabel, value); } }
        void Gotest()
        {
            testLabel = "Work!";
        }
    }
}
