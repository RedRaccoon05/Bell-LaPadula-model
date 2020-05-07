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
namespace Wpf.BLPmodel.Pages.AuthPage.ViewModels
{
    public class AuthViewModel : MasterNavigationViewModel
    {

        public AuthViewModel()
        {
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

        private string _Login, _ErrorMessage;

        public string Login { get { return _Login; } set { SetProperty(ref _Login, value); } }



        public string ErrorMessage { get { return _ErrorMessage; } set { SetProperty(ref _ErrorMessage, value); } }
        public ICommand LoginCommand { get; set; }
        public ICommand RegCommand { get; set; }
        private void Autorize(object shortPageId)
        {

            string result_send = SendtoServerAuth(shortPageId);
            if (ParseNum(result_send))
            {
                SecFlagNavigator = Int32.Parse(result_send);
                Navigator.NavigateTo(PageNames.ThreeView);

            }
            else ErrorMessage = result_send;

        }

        private string SendtoServerAuth(object shortPageId)
        {
            if (_Login != "" && _Login != null)
            {
                PasswordBox pwBox = shortPageId as PasswordBox;
                string pass = pwBox.Password;
                if (pass != "" && pass != null)
                {
                    Authentication.Auth_(_Login, pass);
                    pass = string.Empty;
                    string serdata = Serialize.SerializeAuth(Authentication.data_auth);
                    string result_send = SendData.Send_Data(serdata);
                    UserNameNavigator = _Login;
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
