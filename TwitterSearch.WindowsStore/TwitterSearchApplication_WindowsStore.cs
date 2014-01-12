using Autofac;
using Budgie;
using MvvmPortable.Presentation;
using MvvmPortable.Presentation.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Portable;
using TwitterSearch.Portable.ViewModels;
using TwitterSearch.WindowsStore.Models;
using TwitterSearch.WindowsStore.Views;

namespace TwitterSearch.WindowsStore
{
    public class TwitterSearchApplication_WindowsStore : TwitterSearchApplication
    {
        protected override void RegisterPlatformTypes(ContainerBuilder builder)
        {
            builder.RegisterType<TwitterPlatformAdapter_WindowsStore>().SingleInstance().As<IPlatformAdaptor>();

            var navigationService = new MetroNavigationService(this, new MetroViewBinder());

            navigationService.Register(typeof(HomeViewModel), typeof(HomeView));
            navigationService.Register(typeof(SearchResultsViewModel), typeof(SearchResultsView));

            builder.RegisterInstance(navigationService).As<INavigationService>();
        }
    }
}
