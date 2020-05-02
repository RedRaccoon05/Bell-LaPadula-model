using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.BLPmodel {
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application {

        // переопределяем метов OnStartUp
        protected override void OnStartup(StartupEventArgs e) {

            base.OnStartup(e); // вызываем базе код
            // для проекта был выбран Unity Container

            /* Поехали */
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
