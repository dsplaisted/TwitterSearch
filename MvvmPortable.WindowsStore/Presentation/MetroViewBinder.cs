using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MvvmPortable.Presentation
{
    public class MetroViewBinder : IViewBinder
    {
        public void Bind(object view, object viewModel)
        {
            Requires.NotNull(view, "view");
            Requires.NotNull(viewModel, "viewModel");

            var fe = (FrameworkElement)view;
            fe.DataContext = viewModel;
        }
    }
}