using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Pages.Core;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using Wpf.BLPmodel.Pages.Core.Extentions;
using System.Windows.Controls;
using ClientSide;
using Wpf.BLPmodel.Pages.AuthPage.Views;
namespace Wpf.BLPmodel.Pages.AuthPage.ViewModels {
    public class AuthViewModel : MasterNavigationViewModel {

        public AuthViewModel() {
            // инициализация команды средствами Prism.Command
            LoginCommand = new DelegateCommand<object>(Autorize);
            RegCommand = new DelegateCommand(Reg);
        }
        public override bool KeepAlive
        {
            get
            {
                return true; // страница с меню будет жить в памяти даже когда мы её покинем
            }
        }
        public override bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
            
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
           
        }

        private string _Login, _Password, _ErrorMessage;

        public string Login { get { return _Login; } set { SetProperty(ref _Login, value); } }

        public string Password { get { return _Password; } set { SetProperty(ref _Password, value); } }
        
        public string ErrorMessage { get { return _ErrorMessage; } set { SetProperty(ref _ErrorMessage, value); } }
        public ICommand LoginCommand { get; set; }
        public ICommand RegCommand { get;set; }
        private void Autorize(object shortPageId)
        {
            
            string result_send = SendtoServerAuth(shortPageId);
            if (result_send == "1")
                Navigator.NavigateTo(PageNames.MenuView);
            else ErrorMessage = result_send;

        }

        private string SendtoServerAuth(object shortPageId)
        {
            if (_Login != "" && _Login !=null)
            {
                PasswordBox pwBox = shortPageId as PasswordBox;
                string pass = pwBox.Password;
                if (pass != "" && pass != null)
                {
                    Authentication.Auth_(_Login, pass);//ghbdtn@rfrltkf
                    pass = string.Empty;
                    string serdata = Serialize.SerializeAuth(Authentication.data_auth);
                    string result_send = SendData.Send_Data(serdata);
                    return result_send;
                }
                else return "Введите пароль.";
            }
            else return "Введите логин.";
           
        }

        private void Reg()
        {
            Navigator.NavigateTo(PageNames.OneView);
        }
    }
}
