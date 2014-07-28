using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmPortable;
using MvvmPortable.Composition;

namespace MvvmPortable.Presentation.Navigation
{
    public abstract class NavigationService<TViewId> : INavigationService
    {
        readonly Dictionary<Type, TViewId> _map = new Dictionary<Type, TViewId>();
        readonly IResolver _resolver;
        readonly IViewBinder _viewBinder;

        protected NavigationService(IResolver resolver, IViewBinder viewBinder)
        {
            Requires.NotNull(resolver, "resolver");

            _resolver = resolver;
            _viewBinder = viewBinder;
        }

        public event EventHandler CanGoBackChanged;

        public abstract bool CanGoBack
        {
            get;
        }

        public void GoBack()
        {
            GoBackCore();
            OnCanGoBackChanged(EventArgs.Empty);
        }

        public void Register(Type type, TViewId id)
        {
            _map.Add(type, id);
        }

        public void NavigateTo<TViewModel>(string navigationParameter = null)
            where TViewModel : NavigatableViewModel
        {
            // Get the matching view for this view model
            TViewId id;
            if (!_map.TryGetValue(typeof(TViewModel), out id))
                throw new ArgumentException("No view mapping found for " + typeof(TViewModel).FullName);

            // Call the platform-specific override
            NavigateTo(id, navigationParameter);

            OnCanGoBackChanged(EventArgs.Empty);
        }

        protected void Bind(TViewId id, object view, string navigationParameter = null)
        {
            Type viewModelType = FindViewModelType(id);
            if (viewModelType != null)
            {
                NavigatableViewModel viewModel = (NavigatableViewModel)_resolver.Resolve(viewModelType);
                viewModel.Navigated(navigationParameter);
                _viewBinder.Bind(view, viewModel);
            }
        }

        protected abstract void NavigateTo(TViewId id, string navigationParameter = null);

        protected abstract void GoBackCore();

        protected virtual void OnCanGoBackChanged(EventArgs e)
        {
            var handler = CanGoBackChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private Type FindViewModelType(TViewId locator)
        {
            foreach (var pair in _map)
            {
                if (pair.Value.Equals(locator))
                {
                    return pair.Key;
                }
            }

            return null;
        }
    }
}
