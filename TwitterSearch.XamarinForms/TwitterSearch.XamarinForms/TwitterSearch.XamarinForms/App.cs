using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Infrastructure;
using TwitterSearch.Models;
using TwitterSearch.ViewModels;
using TwitterSearch.Views;
using Xamarin.Forms;

namespace TwitterSearch
{
    public class App
    {
        public static Page GetMainPage()
        {

            return new TwitterSearchApplication().Start();
            //return new ContentPage
            //{
            //    Content = new Label
            //    {
            //        Text = "Hello, Forms !",
            //        VerticalOptions = LayoutOptions.CenterAndExpand,
            //        HorizontalOptions = LayoutOptions.CenterAndExpand,
            //    },
            //};
        }
    }

    public class TwitterSearchApplication : IResolver
    {
        private IContainer _container;

        public Page Start()
        {
            

            NavigationService navigationService = new NavigationService(this);
            navigationService.Register(typeof(HomeViewModel), typeof(HomeView));
            navigationService.Register(typeof(SearchResultsViewModel), typeof(SearchResultsView));

            var builder = new ContainerBuilder();

            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<HomeView>();
            builder.RegisterType<SearchResultsViewModel>().SingleInstance();
            builder.RegisterType<SearchResultsView>();

            builder.RegisterType<Sha1Hasher>().As<Budgie.IPlatformAdaptor>().SingleInstance();
            

            builder.RegisterInstance(this).As<IResolver>();
            builder.RegisterInstance(navigationService).As<INavigationService>();
            

            //  Register your app at dev.twitter.com, then create an implementation of IAuthInformation with your auth keys and secrets
            builder.RegisterType<AuthInformation>().SingleInstance().As<IAuthInformation>();

            _container = builder.Build();

            return navigationService.Init(typeof(HomeViewModel));
        }

        public object Resolve(Type type)
        {
            try
            {
                return _container.Resolve(type);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to resolve type: " + type.FullName + Environment.NewLine + ex.ToString());
                throw;
            }
        }
    }

}
