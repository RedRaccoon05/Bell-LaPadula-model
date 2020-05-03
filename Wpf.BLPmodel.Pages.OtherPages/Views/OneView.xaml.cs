using System;
using System.Collections.Generic;
using System.Linq;
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
      

    }
    public class BindablePasswordBox : Decorator
    {
        public static readonly DependencyProperty PasswordProperty
            = DependencyProperty.Register(nameof(Password), typeof(string), typeof(BindablePasswordBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

        static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            var bpb = (BindablePasswordBox)d;
            if (bpb.isPreventCallback) return;
            bpb.passwordBox.PasswordChanged -= bpb.HandlePasswordChanged;
            bpb.passwordBox.Password = (string)eventArgs.NewValue;
            bpb.passwordBox.PasswordChanged += bpb.HandlePasswordChanged;
        }

        bool isPreventCallback = false;
        PasswordBox passwordBox = new PasswordBox();

        public BindablePasswordBox()
        {
            passwordBox.PasswordChanged += HandlePasswordChanged;
            Child = passwordBox;
        }

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        void HandlePasswordChanged(object sender, RoutedEventArgs eventArgs)
        {
            isPreventCallback = true;
            Password = passwordBox.Password;
            isPreventCallback = false;
        }
    }
    public class PasswordValidator : FrameworkElement
    {
        static IDictionary<PasswordBox, Brush> _passwordBoxes = new Dictionary<PasswordBox, Brush>();

        public static readonly DependencyProperty Box1Property = DependencyProperty.Register("Box1", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box1Changed));
        public static readonly DependencyProperty Box2Property = DependencyProperty.Register("Box2", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box2Changed));
        public static readonly DependencyProperty ButtProperty = DependencyProperty.Register("Butt", typeof(Button), typeof(PasswordValidator), new PropertyMetadata(ButtChanged));
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label_Err", typeof(Label), typeof(PasswordValidator), new PropertyMetadata(Label_ErrChanged));

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

        private static void CheckValidator(DependencyObject d)
        {
            var pv = (PasswordValidator)d;
            _passwordBoxes[pv.Box2] = pv.Box2.BorderBrush;
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

        private static void Box2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CheckValidator(d);
        }
    }
}
