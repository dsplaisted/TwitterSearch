using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using MvvmPortable.Composition;

namespace MvvmPortable.Presentation.Navigation
{
    // Phone's implementation of a navigation service that
    // navigates from a ViewModel type to a Uri
    public class PhoneNavigationService : NavigationService<Uri>
    {
        public PhoneNavigationService(IResolver resolver, IViewBinder viewBinder, Frame frame)
            : base(resolver, viewBinder)
        {
            Frame = frame;
            Frame.Navigated += OnFrameNavigated;

            //Register(typeof(ShoppingListViewModel),     new Uri("/Views/ShoppingListView.xaml",    UriKind.Relative));
            //Register(typeof(AddGroceryItemViewModel),   new Uri("/Views/AddGroceryItemView.xaml",  UriKind.Relative));
        }

        public Frame Frame { get; private set; }
        //public Frame Frame
        //{
        //    get { return (Frame)MvvmPortablePhoneHost.Frame; }
        //}

        public override bool CanGoBack
        {
            get { return Frame.CanGoBack; }
        }

        protected override void GoBackCore()
        {
            Frame.GoBack();
        }

        protected override void NavigateTo(Uri url, string navigationParameter = null)
        {
            if (!string.IsNullOrEmpty(navigationParameter))
            {
                url = new Uri(url.ToString() + "?parameter=" + navigationParameter, UriKind.Relative);
            }
            if (Frame.CurrentSource == url)
            {
                //  Handle startup scenario where the initial page has already been navigated to

                OnFrameNavigated(this, new NavigationEventArgs(Frame.Content, url));

            }
            else
            {
                Frame.Navigate(url);
            }
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            //  Content will be null when navigating away from the app (for example when pressing the start button)
            if (e.NavigationMode == NavigationMode.New && e.Content != null)
            {
                var view = (Page)e.Content;
                string parameter = null;
                if (view.NavigationContext.QueryString.ContainsKey("parameter"))
                {
                    parameter = view.NavigationContext.QueryString["parameter"];
                }

                Uri plainUri = e.Uri;
                int queryStringStart = e.Uri.OriginalString.IndexOf('?');
                if (queryStringStart >= 0)
                {
                    plainUri = new Uri(e.Uri.OriginalString.Substring(0, queryStringStart), UriKind.Relative);
                }

                Bind(plainUri, view, parameter);
            }
        }
    }
}
