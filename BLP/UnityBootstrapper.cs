using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System.Windows;
using Wpf.BLPmodel.Modules;
namespace Wpf.BLPmodel
{
    public class Bootstrapper : UnityBootstrapper
    {

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellWindow>();
            // указываем какое окно будет Shell 
        }

        protected override void InitializeShell()
        {
            // переопределяем инициализацию окна
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // КОнфигурируем модули

            // у нас он один
            this.ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "Navigation",
                ModuleType = typeof(NavigationModule.NavigationModule).AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // ранее мы инициализировали модель
            // теперь я ещё зарегистрировал его как сервис навигации
            Container.RegisterType<INavigationModule, NavigationModule.NavigationModule>();
        }
    }
}
