using Autofac;
using Budgie;
using MvvmPortable.Presentation;
using MvvmPortable.Presentation.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TwitterSearch.Portable;
using TwitterSearch.Portable.ViewModels;
using TwitterSearch.WindowsPhone.Models;

namespace TwitterSearch.WindowsPhone
{
    public class TwitterSearchApplication_WindowsPhone : TwitterSearchApplication
    {
        Frame _navigationFrame;
        PhoneNavigationService _navigationService;

        public TwitterSearchApplication_WindowsPhone(Frame navigationFrame)
        {
            _navigationFrame = navigationFrame;
        }

        protected override void RegisterPlatformTypes(ContainerBuilder builder)
        {
            builder.RegisterType<TwitterPlatformAdapter_WindowsPhone>().SingleInstance().As<IPlatformAdaptor>();

            _navigationService = new PhoneNavigationService(this, new PhoneViewBinder(), _navigationFrame);

            _navigationService.Register(typeof(HomeViewModel), new Uri("/Views/HomeView.xaml", UriKind.Relative));
            _navigationService.Register(typeof(SearchResultsViewModel), new Uri("/Views/SearchResultsView.xaml", UriKind.Relative));

            builder.RegisterInstance(_navigationService).As<INavigationService>();
        }
    }
}
