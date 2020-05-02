using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Pages.Core.Extentions;

namespace Wpf.BLPmodel.Modules {
    public interface INavigationModule : IModule {

        // были добавлено пара методов которые будт отвечать за навигацию.

            /// <summary>
            /// без параметров
            /// </summary>
            /// <param name="pageName"></param>
        void NavigateTo(PageNames pageName);

        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="parameters"></param>
        void NavigateTo(PageNames pageName, NavigationParameters parameters);

    }
}
