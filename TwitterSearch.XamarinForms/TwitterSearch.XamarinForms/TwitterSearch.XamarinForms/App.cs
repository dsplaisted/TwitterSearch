using Autofac;
using System;
using System.Collections.Generic;
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

        public NavigationPage Start()
        {
            NavigationPage ret = new NavigationPage();

            NavigationService navigationService = new NavigationService(ret, this);
            navigationService.Register(typeof(HomeViewModel), typeof(HomeView));

            var builder = new ContainerBuilder();

            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<SearchResultsViewModel>().SingleInstance();
            builder.RegisterType<HomeView>();
            

            builder.RegisterInstance(this).As<IResolver>();
            builder.RegisterInstance(navigationService).As<INavigationService>();
            

            //  Register your app at dev.twitter.com, then create an implementation of IAuthInformation with your auth keys and secrets
            builder.RegisterType<AuthInformation>().SingleInstance().As<IAuthInformation>();

            _container = builder.Build();

            navigationService.NavigateToAsync<HomeViewModel>();
            
            return ret;
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }

}
