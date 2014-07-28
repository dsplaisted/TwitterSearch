using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Infrastructure
{
    public interface INavigationService
    {
        //Task<TViewModel>
        Task
            NavigateToAsync<TViewModel>(string navigationParameter = null)
            where TViewModel : BaseViewModel;
    }
}
