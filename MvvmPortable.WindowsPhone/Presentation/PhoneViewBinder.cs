using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.ComponentModel;

namespace MvvmPortable.Presentation
{
    public class PhoneViewBinder : IViewBinder
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
