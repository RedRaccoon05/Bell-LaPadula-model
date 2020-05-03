using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Pages.Core;
using System.Windows;
using Wpf.BLPmodel.Pages.Core.Extentions;
using Microsoft.Practices.Prism.Commands;
using ClientSide;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels
{
    public class OneViewModel : MasterNavigationViewModel
    {



        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //var testParam = navigationContext.Parameters["TestParam"].ToString();
            //MessageBox.Show(String.Format("тестовый параметра:{0}", testParam));
        }

        // данный метод и в контектсте перехода мы можем получить доступ к данному параметру
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //var testParam = navigationContext.Parameters["TestParam"].ToString();
            //MessageBo x.Show(String.Format("тестовый параметра:{0}", testParam));
        }

 

        private string _Login, _ErrorMessage;

        public string Login { get { return _Login; } set { SetProperty(ref _Login, value); } }

        public string ErrorMessage { get { return _ErrorMessage; } set { SetProperty(ref _ErrorMessage, value); } }

        //public string Password { get { return _Password; } set { SetProperty(ref _Password, value); } }

        //public string ConfirmPassword { get { return _ConfirmPassword; } set { SetProperty(ref _ConfirmPassword, value); } }

        public OneViewModel()
        {
            GoBackCommand = new DelegateCommand(GoBack);
            GoRegCommand = new DelegateCommand<object>(GoReg);

        }
        public ICommand GoBackCommand { get; set; }

        public ICommand GoRegCommand { get; set; }
   
        private void GoBack()
        {
            Navigator.NavigateTo(PageNames.AuthView);
        }

        private void GoReg(object shortPageId)
        {
            ErrorMessage = "";
            string result_send =   SendtoServerReg(shortPageId);
            if (result_send == "Ok")
                GoBack();
            else ErrorMessage = result_send;
        }


        private string SendtoServerReg(object shortPageId)
        {
            if (_Login != "" && _Login != null)
            {
                PasswordBox pwBox = shortPageId as PasswordBox;
                string pass = pwBox.Password;

              

                if (pass != "" && pass != null)
                {
                    if (Registration.Check_Pass(pass))
                    {
                        Registration.Reg_(_Login, pass);
                        pass = string.Empty;
                        string serdata = Serialize.SerializeAuth(Registration.data_reg);
                        string result_send = SendData.Send_Data(serdata);
                        return result_send;

                    }
                    else return "Пароль не соответствует требованиям безопасности";
                 
                }
                else return "Введите пароль.";
            }
            else return "Введите логин.";

        }
    }
}
