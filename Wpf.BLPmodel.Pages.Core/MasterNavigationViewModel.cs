using System;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Wpf.BLPmodel.Modules;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel;

using Microsoft.Practices.ServiceLocation;
using ClientSide;

/*
 Наша базовая модель представления
 BindableBase - MVVM реализация из PRISM.Mvvm
 INavigationAware - Prism.Regions (Отвечает за навигацию)

    добавлен
 IRegionMemberLifetime - интерфейс для утсановки жизна формы при её закрытии или переходе на другую страницу.
 KeepAlive = false , форма будет удалена из региона при переходе на другую форму. 
*/

namespace Wpf.BLPmodel.Pages.Core
{
   

    /*Класс объявлен абстрактным, нельзя стоздать его экземпляр только наследоваться или привести к нему */
    public abstract class MasterNavigationViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {

        /* добавлен достап к нашему сервису навигации, что бы каждый раз в модели представления его не вызывать */
        protected INavigationModule Navigator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<INavigationModule>();
            }
        }
        static public Data_Note NoteToRead = new Data_Note() ;
         
        static protected int Back_to_Grid_Flag = 0;
        static protected string UserNameNavigator;
        static protected int SecFlagNavigator;
        public virtual bool KeepAlive
        {
            get
            {
                return false; // для всех форм по умолчанию false / но можно переопределить
            }
        }

        // При навигации к форме вызывается и в данном методе можно рповерить сможет ли форма обработать данный запрос
        public abstract bool IsNavigationTarget(NavigationContext navigationContext);

        // Вызывается когда происходит навигация из данной формы в другую форму
        public abstract void OnNavigatedFrom(NavigationContext navigationContext);

        // Вызывается при навигации на данныю форму
        public abstract void OnNavigatedTo(NavigationContext navigationContext);
        public bool ParseNum(string message)
        {
            int x = 0;
            Int32.TryParse(message, out x);
            return (x != 0) ? true : false;
        }
    }
}
