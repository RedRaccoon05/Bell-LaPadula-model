using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Wpf.BLPmodel.Modules;
using Wpf.BLPmodel.Pages.AuthPage.ViewModels;
using Wpf.BLPmodel.Pages.AuthPage.Views;
using Wpf.BLPmodel.Pages.Core.Extentions;

namespace Wpf.BLPmodel.NavigationModule
{
    public class NavigationModule : INavigationModule
    {


        // реализация навигации

        public void NavigateTo(PageNames pageName)
        {
            NavigateTo(pageName, null);
        }


        public void NavigateTo(PageNames pageName, NavigationParameters parameters)
        {
            // сначала проверяем не присутствует ли наша страница в регионе,
            // если не присутствует, запускаем добавление
            if (_RegionManager.Regions["RegionShell"].GetView(pageName.ToString()) == null)
            {
                Helpers.InitializeViewHelper.Run(pageName, _UnityContainer, _RegionManager);
            }

            // после добавления формы в регион переходи на данную форму с передачей параметров
            // добавлю один параметр
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }
            parameters.Add("TestParam", "Это тестовый параметр");

            _RegionManager.Regions["RegionShell"].RequestNavigate(pageName.ToString(), parameters);
        }











        private readonly IRegionManager _RegionManager;
        private readonly IUnityContainer _UnityContainer;

        public NavigationModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this._RegionManager = regionManager;
            _UnityContainer = unityContainer;
        }

        public void Initialize()
        {
            // при инициализации модуля в RegionShell будет добавлена наша страница авторизации
            var view = _UnityContainer.Resolve<AuthView>();
            //её модель представления
            view.DataContext = _UnityContainer.Resolve<AuthViewModel>();
            _RegionManager.Regions["RegionShell"].Add(view);
        }
    }
}
