using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Infrastructure;
using TwitterSearch.Models;
using TwitterSearch.ViewModels;
using Xamarin.Forms;

namespace TwitterSearch
{
    public class App
    {
        public static Page GetMainPage()
        {
            return new ContentPage
            {
                Content = new Label
                {
                    Text = "Hello, Forms !",
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                },
            };
        }
    }

    public abstract class TwitterSearchApplication : IResolver
    {
        private IContainer _container;

        protected abstract void RegisterPlatformTypes(ContainerBuilder builder);

        public async virtual Task StartAsync()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HomeViewModel>().SingleInstance();
            builder.RegisterType<SearchResultsViewModel>().SingleInstance();
            

            builder.RegisterInstance(this).As<IResolver>();

            //  Register your app at dev.twitter.com, then create an implementation of IAuthInformation with your auth keys and secrets
            builder.RegisterType<AuthInformation>().SingleInstance().As<IAuthInformation>();

            RegisterPlatformTypes(builder);

            _container = builder.Build();

            INavigationService navigation = _container.Resolve<INavigationService>();
            navigation.NavigateTo<HomeViewModel>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }

}
