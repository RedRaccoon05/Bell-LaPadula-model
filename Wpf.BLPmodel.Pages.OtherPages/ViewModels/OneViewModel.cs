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

        public class FindCommandParameters
        {
            public string Text { get; set; }
            public bool IgnoreCase { get; set; }
        }
        public class MyMultiConverter : IMultiValueConverter
        {

            public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                FindCommandParameters parameters = new FindCommandParameters();
                foreach (var obj in values)
                {
                    if (obj is string) parameters.Text = (string)obj;
                    else if (obj is bool) parameters.IgnoreCase = (bool)obj;
                }
                return parameters;
            }
            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        private string _Login, _Password, _ConfirmPassword;

        public string Login { get { return _Login; } set { SetProperty(ref _Login, value); } }

        public string Password { get { return _Password; } set { SetProperty(ref _Password, value); } }

        public string ConfirmPassword { get { return _ConfirmPassword; } set { SetProperty(ref _ConfirmPassword, value); } }

        public OneViewModel()
        {
            GoBackCommand = new DelegateCommand(GoBack);

        }
        public ICommand GoBackCommand { get; set; }

        public ICommand GoRegCommand { get; set; }
   
        private void GoBack()
        {
            Navigator.NavigateTo(PageNames.AuthView);
        }

        private void GoReg()
        {

        }


        private string SendtoServerReg(object shortPageId, object shortPageId_confirm)
        {
            if (_Login != "" && _Login != null)
            {
                PasswordBox pwBox = shortPageId as PasswordBox;
                string pass = pwBox.Password;

                PasswordBox pwBox_confirm = shortPageId_confirm as PasswordBox;
                string pass_confirm = pwBox_confirm.Password;


                if (pass != "" && pass != null)
                {
                    if (pass_confirm != "" && pass_confirm != null)
                    {
                        if (pass == pass_confirm)
                        {
                            Registration.Reg_(_Login, pass);
                            pass = string.Empty;
                            pass_confirm = string.Empty;
                            string serdata = Serialize.SerializeAuth(Registration.data_reg);
                            string result_send = SendData.Send_Data(serdata);
                            return result_send;
                        }
                        else return "Пароли не совпадают";
                    }
                    else return "Подтвериде пароль";
                }
                else return "Введите пароль.";
            }
            else return "Введите логин.";

        }
    }
}
