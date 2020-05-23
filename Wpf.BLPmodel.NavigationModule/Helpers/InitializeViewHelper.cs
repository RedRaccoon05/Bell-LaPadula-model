using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Windows.Controls;
using Wpf.BLPmodel.Pages.Core.Extentions;
using Wpf.BLPmodel.Pages.OtherPages.ViewModels;
using Wpf.BLPmodel.Pages.OtherPages.Views;

namespace Wpf.BLPmodel.NavigationModule.Helpers
{
    public static class InitializeViewHelper
    {

        // тут соответственно добавляется форма в регион
        public static void Run(PageNames pageName, IUnityContainer unityContainer, IRegionManager regionManager)
        {
            switch (pageName)
            {
                case PageNames.OneView:
                    regionManager.Regions["RegionShell"].Add(GetOneView(unityContainer));
                    return;
                case PageNames.TwoView:
                    regionManager.Regions["RegionShell"].Add(GetTwoView(unityContainer));
                    return;
                case PageNames.ThreeView:
                    regionManager.Regions["RegionShell"].Add(GetThreeView(unityContainer));
                    return;
            }
        }

        private static UserControl GetOneView(IUnityContainer unityContainer)
        {
            var view = unityContainer.Resolve<OneView>();
            view.DataContext = unityContainer.Resolve<OneViewModel>();
            return view;
        }

        private static UserControl GetTwoView(IUnityContainer unityContainer)
        {
            var view = unityContainer.Resolve<TwoView>();
            view.DataContext = unityContainer.Resolve<TwoViewModel>();
            return view;
        }

        private static UserControl GetThreeView(IUnityContainer unityContainer)
        {
            var view = unityContainer.Resolve<ThreeView>();
            view.DataContext = unityContainer.Resolve<ThreeViewModel>();
            return view;
        }

    }
}
