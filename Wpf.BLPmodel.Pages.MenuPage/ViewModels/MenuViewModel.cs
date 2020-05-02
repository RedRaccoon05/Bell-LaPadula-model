using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Pages.Core;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Wpf.BLPmodel.Pages.Core.Extentions;
using System.Timers;

namespace Wpf.BLPmodel.Pages.MenuPage.ViewModels {
    public class MenuViewModel : MasterNavigationViewModel {

        public MenuViewModel() {
            GoToCommand = new DelegateCommand<object>(GoTo);    
        }

        public override bool KeepAlive {
            get {
                return true; // страница с меню будет жить в памяти даже когда мы её покинем
            }
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {

        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            // добавлен таймер для проверки не выгружалась ли наша страница из памяти приложения
            if (T == null) {
                T = new Timer();
                T.Interval = 1000;
                T.Elapsed += T_Elapsed;
                T.Start();
            }
            // таймер начнет работу при первом переходе на данную страницу
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e) {
            if(TikTak != long.MaxValue) {
                TikTak += 1;
            }
            else {
                TikTak = 0;
            }
        }

        Timer T;

        private long _TikTak = 0;

        public long TikTak {
            get { return _TikTak; }
            set { SetProperty(ref _TikTak, value); }
        }


        public ICommand GoToCommand { get; set; }

        private void GoTo(object shortPageId) {
            Navigator.NavigateTo((PageNames)Convert.ToInt32(shortPageId));
        }

    }
}
