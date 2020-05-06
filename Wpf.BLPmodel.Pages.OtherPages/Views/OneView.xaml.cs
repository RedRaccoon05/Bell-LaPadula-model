using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientSide;
using Wpf.BLPmodel.Pages.Core.Extentions;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Pages.Core;


using Wpf.BLPmodel.Modules;
namespace Wpf.BLPmodel.Pages.OtherPages.Views
{
    /// <summary>
    /// Логика взаимодействия для OneView.xaml
    /// </summary>
    public partial class OneView : UserControl
    {
        public OneView()
        {
            InitializeComponent();

        }



    }

    public class PasswordValidator : FrameworkElement
    {
        static IDictionary<PasswordBox, Brush> _passwordBoxes = new Dictionary<PasswordBox, Brush>();

        public static readonly DependencyProperty Box1Property = DependencyProperty.Register("Box1", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box1Changed));
        public static readonly DependencyProperty Box2Property = DependencyProperty.Register("Box2", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box2Changed));
        public static readonly DependencyProperty ButtProperty = DependencyProperty.Register("Butt", typeof(Button), typeof(PasswordValidator), new PropertyMetadata(ButtChanged));
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label_Err", typeof(Label), typeof(PasswordValidator), new PropertyMetadata(Label_ErrChanged));
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(int), typeof(PasswordValidator), new PropertyMetadata(ModeChanged));
        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(Label), typeof(PasswordValidator), new PropertyMetadata(UserNameChanged));

        public PasswordBox Box1
        {
            get { return (PasswordBox)GetValue(Box1Property); }
            set { SetValue(Box1Property, value); }
        }
        public PasswordBox Box2
        {
            get { return (PasswordBox)GetValue(Box2Property); }
            set { SetValue(Box2Property, value); }
        }
        public Button Butt
        {
            get { return (Button)GetValue(ButtProperty); }
            set { SetValue(ButtProperty, value); }
        }
        public Label Label_Err
        {
            get { return (Label)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public int Mode
        {
            get { return (int)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        public Label UserName
        {
            get { return (Label)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }
        private static void ModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void UserNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void Box1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void Label_ErrChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void ButtChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckValidator(d);
        }
       static private bool ParseNum(string message)
        {
            int x = 0;
            Int32.TryParse(message, out x);
            return (x != 0) ? true : false;
        }
        private static void CheckValidator(DependencyObject d)
        {
            var pv = (PasswordValidator)d;
            _passwordBoxes[pv.Box2] = pv.Box2.BorderBrush;
            if (pv.Mode == 0)
            {
                pv.Box2.LostFocus += (obj, evt) =>
                {
                    if (pv.Box1.Password != pv.Box2.Password)
                    {
                        pv.Box2.BorderBrush = new SolidColorBrush(Colors.Red);
                        pv.Butt.IsEnabled = false;
                        pv.Label_Err.Content = "Пароль не совпадает";
                    }
                    else
                    {
                        pv.Box2.BorderBrush = _passwordBoxes[pv.Box2];
                        pv.Butt.IsEnabled = true;
                        pv.Label_Err.Content = "";
                    }
                };

            }
            else if (pv.Mode == 1)
            {
                pv.Box1.LostFocus += (obj, evt) =>
                {
                    if (pv.Box2.Password == pv.Box1.Password) 
                        pv.Label_Err.Content = "Новый пароль совпадает со старым";
                   else if(pv.Box2.Password != pv.Box1.Password)
                    {

                        pv.Label_Err.Content = "";
                        Authentication.Auth_(pv.UserName.Content.ToString(), pv.Box1.Password);
                       
                        string serdata = Serialize.SerializeAuth(Authentication.data_auth);
                        string result_send = SendData.Send_Data(serdata);
                        if (ParseNum(result_send))
                        {
                            pv.Butt.IsEnabled = true;
                        }
                        else pv.Butt.IsEnabled = false;
                    }
                    

                };
            }
        }

        private static void Box2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckValidator(d);
        }
       
    }
}
