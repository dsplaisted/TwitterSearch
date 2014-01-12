using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmPortable.Presentation
{
    public interface IViewBinder
    {
        void Bind(object view, object viewModel);
    }
}
