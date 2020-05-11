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
using Wpf.BLPmodel.Pages.OtherPages.ViewModels;

namespace Wpf.BLPmodel.Pages.OtherPages.Views
{
    /// <summary>
    /// Interaction logic for NotesGridView.xaml
    /// </summary>
    public partial class NotesGridView : UserControl
    {
        public NotesGridView()
        {
            InitializeComponent();
          
        }
        //private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    DataGridRow row = sender as DataGridRow;
        //    var ehhh = row.Item.ToString();
        //    DataContext = new FourViewModel();
            
        //    MessageBox.Show("NICE");
        //}
    }
}


