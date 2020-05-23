using ClientSide;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Wpf.BLPmodel.Pages.OtherPages.ViewModels
{
    public class TwoViewModel : BaseNumericViewModel
    {

        public TwoViewModel()
        {
            ChangePasswordCommand = new DelegateCommand<object>(ChangePassword_);
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
            GetUserName();
            GetSecFlag();
        }
        public ICommand ChangePasswordCommand { get; set; }

        private void ChangePassword_(object obj)//Функция смены пароля
        {
            PasswordBox newpass = obj as PasswordBox;
            if (newpass.Password != null && newpass.Password != "")
            {
                if (Registration.Check_Pass(newpass.Password))
                {

                    ChangePassword.ShangePass_(UserNameNavigator, newpass.Password);
                    string serdata = Serialize.SerializeAuth(ChangePassword.data_change_pass);
                    string result_send = SendData.Send_Data(serdata);
                    if (result_send == "Ok")
                        MessageBox.Show(String.Format("Пароль изменен"));

                }
                else
                {

                    MessageBox.Show(String.Format("Новый пароль не соответствует требованию безопасности"));
                }
            }
            else
            {
                MessageBox.Show(String.Format("Введите новый пароль"));
            }

        }
        private string _UserName;
        private int _SecFlag;
        public string UserName { get { return _UserName; } set { SetProperty(ref _UserName, value); } }
        public int SecFlag { get { return _SecFlag; } set { SetProperty(ref _SecFlag, value); } }
        void GetUserName()//Получения имени пользователя для отображения на странице
        {
            _UserName = UserNameNavigator;
        }
        void GetSecFlag()//Получения уровня доступа пользователя для отображения на странице
        {
            _SecFlag = SecFlagNavigator;
        }

    }
}
