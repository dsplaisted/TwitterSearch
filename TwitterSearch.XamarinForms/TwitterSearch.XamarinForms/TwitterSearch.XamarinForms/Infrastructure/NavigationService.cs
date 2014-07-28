using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TwitterSearch.Infrastructure
{
    class NavigationService : INavigationService
    {
        readonly IResolver _resolver;

        readonly Dictionary<Type, Type> _vmToViewMap = new Dictionary<Type, Type>();

        NavigationPage _navigationPage;

        public NavigationService(IResolver resolver)
        {
            _resolver = resolver;
        }

        public void Register(Type viewModelType, Type viewType)
        {
            _vmToViewMap.Add(viewModelType, viewType);
        }

        void CreateViewAndViewModel(Type viewModelType, out Page view, out BaseViewModel viewModel, string navigationParameter)
        {
            // Get the matching view for this view model
            Type viewType;
            if (!_vmToViewMap.TryGetValue(viewModelType, out viewType))
                throw new ArgumentException("No view mapping found for " + viewModelType.FullName);

            view = (Page)_resolver.Resolve(viewType);

            viewModel = (BaseViewModel)_resolver.Resolve(viewModelType);
            viewModel.Navigated(navigationParameter);

            view.BindingContext = viewModel;

            Debug.WriteLine("End of CreateViewAndViewModel");

        }

        //public async Task<TViewModel> NavigateToAsync<TViewModel>(string navigationParameter = null)
        public Task NavigateToAsync<TViewModel>(string navigationParameter = null)
            where TViewModel : BaseViewModel
        {
            //// Get the matching view for this view model
            //Type viewType;
            //if (!_vmToViewMap.TryGetValue(typeof(TViewModel), out viewType))
            //    throw new ArgumentException("No view mapping found for " + typeof(TViewModel).FullName);

            //Page viewPage = (Page)_resolver.Resolve(viewType);

            //BaseViewModel viewModel = (BaseViewModel)_resolver.Resolve(typeof(TViewModel));
            //viewModel.Navigated(navigationParameter);

            //viewPage.BindingContext = viewModel;

            if (_navigationPage == null)
            {
                throw new InvalidOperationException("Navigation not initialized.");
            }

            Page viewPage;
            BaseViewModel viewModel;
            CreateViewAndViewModel(typeof(TViewModel), out viewPage, out viewModel, navigationParameter);

            Debug.WriteLine("About to navigate");
            //await _navigationPage.PushAsync(viewPage);
            //Debug.WriteLine("Navigated");
            //return (TViewModel) viewModel;

            return _navigationPage.PushAsync(viewPage);
            
        }

        public NavigationPage Init(Type rootViewModelType)
        {
            Page viewPage;
            BaseViewModel viewModel;
            CreateViewAndViewModel(rootViewModelType, out viewPage, out viewModel, null);

            var ret = new NavigationPage(viewPage);
            _navigationPage = ret;

            return ret;
        }
    }
}
