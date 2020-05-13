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
    public enum GetNote : int
    {
        Grid, Read, Write
    }
    static class Mode
    {
        static public GetNote flag1 = GetNote.Grid;
    }

    public partial class ThreeView : UserControl
    {


        public ThreeView()
        {

            InitializeComponent();
            //   DataContext = new TwoViewModel();
            if (Mode.flag1 == GetNote.Grid)
            {
                sd.DataContext = new NotesGridViewModel();
                
            }
            else if (Mode.flag1 == GetNote.Read)
            {
               
                sd.DataContext = new ReadNoteViewModel();

            } 
            else if (Mode.flag1 == GetNote.Write)
            {
                sd.DataContext = new WriteNoteView();
            }

        }
        public void ReadNote()
        {
            sd.DataContext = new ReadNoteViewModel();
        }

        private void sd_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }

}
