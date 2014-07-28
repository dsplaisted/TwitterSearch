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
        readonly INavigation _navigation;
        readonly IResolver _resolver;

        readonly Dictionary<Type, Type> _vmToViewMap = new Dictionary<Type, Type>();

        public NavigationService(INavigation navigation, IResolver resolver)
        {
            _navigation = navigation;
            _resolver = resolver;
        }

        public void Register(Type viewModelType, Type viewType)
        {
            _vmToViewMap.Add(viewModelType, viewType);
        }

        public async Task<TViewModel> NavigateToAsync<TViewModel>(string navigationParameter = null)
            where TViewModel : BaseViewModel
        {
            // Get the matching view for this view model
            Type viewType;
            if (!_vmToViewMap.TryGetValue(typeof(TViewModel), out viewType))
                throw new ArgumentException("No view mapping found for " + typeof(TViewModel).FullName);

            Page viewPage = (Page)_resolver.Resolve(viewType);

            BaseViewModel viewModel = (BaseViewModel)_resolver.Resolve(typeof(TViewModel));
            viewModel.Navigated(navigationParameter);

            viewPage.BindingContext = viewModel;

            await _navigation.PushAsync(viewPage);

            return (TViewModel) viewModel;
            
        }
    }
}
