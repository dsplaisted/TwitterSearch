using System;
using MvvmPortable.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MvvmPortable.Presentation.Navigation
{
    // Metro's implementation of a navigation service that 
    // navigates from a ViewModel type to a View type
    public class MetroNavigationService : NavigationService<Type>
    {
        public MetroNavigationService(IResolver resolver, IViewBinder viewBinder)
            : base(resolver, viewBinder)
        {
            Frame.Navigated += OnFrameNavigated;

            //Register(typeof(ShoppingListViewModel),     typeof(ShoppingListView));
            //Register(typeof(AddGroceryItemViewModel),   typeof(AddGroceryItemView));
        }

        public Frame Frame
        {
            get { return (Frame)Window.Current.Content; }
        }

        public override bool CanGoBack
        {
            get { return Frame.CanGoBack; }
        }

        protected override void NavigateTo(Type viewType, string navigationParameter = null)
        {
            Frame.Navigate(viewType, navigationParameter);
        }

        protected override void GoBackCore()
        {
            Frame.GoBack();
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            Bind(e.SourcePageType, e.Content, (string) e.Parameter);
        }
    }
}
