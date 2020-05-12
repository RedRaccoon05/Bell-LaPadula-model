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
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Modules;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel;
namespace Wpf.BLPmodel.Pages.OtherPages.Views
{
    /// <summary>
    /// Логика взаимодействия для ThreeView.xaml
    /// </summary>
    public partial class ThreeView : UserControl
    {
        static int de=0;
        public ThreeView()
        {
          
            InitializeComponent();
            //   DataContext = new TwoViewModel();
            if (de == 0)
            {
                sd.DataContext = new NotesGridViewModel();
                de++;
            }
            else sd.DataContext = new ReadNoteViewModel();
        }
        public void ReadNote()
        {
            sd.DataContext = new ReadNoteViewModel();
        }

        private void sd_Navigated(object sender, NavigationEventArgs e) {

        }
    }

}
