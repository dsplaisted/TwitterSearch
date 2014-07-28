using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TwitterSearch.Infrastructure
{
    class NavigationService : INavigationService
    {
        INavigation _navigation;

        public NavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        public void NavigateTo<TViewModel>(string navigationParameter = null) where TViewModel : BaseViewModel
        {
            throw new NotImplementedException();
        }
    }
}
