using MvvmPortable.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autofac;
using MvvmPortable.Composition;
using System.Threading;
using MvvmPortable.Presentation.Navigation;
using MvvmPortable.Eventing;
using TwitterSearch.Portable.ViewModels;
using Budgie;
using TwitterSearch.Portable.Models;

namespace TwitterSearch.Portable
{
    public abstract class TwitterSearchApplication : IResolver
    {
        private IContainer _container;

        private IEnumerable<Type> ServiceTypes(Type t)
        {
            foreach (Type type in t.GetTypeInfo().ImplementedInterfaces)
                yield return type;

            if (t.GetTypeInfo().BaseType != typeof(object))
                yield return t.GetTypeInfo().BaseType;

            yield return t;
        }

        protected abstract void RegisterPlatformTypes(ContainerBuilder builder);

        public async virtual Task StartAsync()
        {
            var assemblies = new[] { typeof(TwitterSearchApplication), typeof(IViewBinder)}
                .Select(type => type.GetTypeInfo().Assembly).Distinct().ToArray();

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(assemblies)
                .SingleInstance()
                .As(t => ServiceTypes(t));

            builder.RegisterInstance(this).As<IResolver>();
            builder.RegisterInstance(SynchronizationContext.Current).As<SynchronizationContext>();

            //  Register your app at dev.twitter.com, then create an implementation of IAuthInformation with your auth keys and secrets
            builder.RegisterType<AuthInformation>().SingleInstance().As<IAuthInformation>();

            RegisterPlatformTypes(builder);

            _container = builder.Build();

            // Create all the startup services
            _container.Resolve<IEnumerable<IStartupService>>();

            //await LoadStateAsync();

            INavigationService navigation = _container.Resolve<INavigationService>();
            navigation.NavigateTo<HomeViewModel>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
