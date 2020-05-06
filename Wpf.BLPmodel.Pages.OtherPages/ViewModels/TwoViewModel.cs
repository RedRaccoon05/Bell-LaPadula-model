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
namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels {
    public class TwoViewModel : BaseNumericViewModel {

        public TwoViewModel() {
            ChangePasswordCommand = new DelegateCommand<object>(ChangePassword_);
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
           
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            GetUserName();
        }
        public ICommand ChangePasswordCommand { get; set; }

        private void ChangePassword_(object ob)
        {
            PasswordBox b = ob as PasswordBox;

            if (Registration.Check_Pass(b.Password))
            {

                ChangePassword.ShangePass_(Logg, b.Password);
                string serdata = Serialize.SerializeAuth(ChangePassword.data_change_pass);
                string result_send = SendData.Send_Data(serdata);
                if(result_send == "Ok")
                    MessageBox.Show(String.Format("Пароль изменен"));

            }
            else
            {
              
                MessageBox.Show(String.Format("Новый пароль не соответствует требованию безопасности"));
            }

        }
        private string _UserName;
        public string UserName { get { return _UserName; } set { SetProperty(ref _UserName, value); } }
        void GetUserName()
        {
            _UserName = Logg;
        }
    }
}
