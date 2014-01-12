using MvvmPortable.Presentation;
using MvvmPortable.Presentation.Input;
using MvvmPortable.Presentation.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TwitterSearch.Portable.ViewModels
{
    public class HomeViewModel : NavigatableViewModel
    {
        Random _rnd = new Random();
        readonly INavigationService _navigationService;

        public HomeViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            //PickRandomSearchTerm();
            SearchText = "MvvmCross";
        }

        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        ActionCommand _searchCommand;
        public ICommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new ActionCommand(Search)); }
        }

        ActionCommand _pickRandomCommand;
        public ICommand PickRandomCommand
        {
            get { return _pickRandomCommand ?? (_pickRandomCommand = new ActionCommand(PickRandomSearchTerm)); }
        }

        void Search()
        {
            _navigationService.NavigateTo<SearchResultsViewModel>(SearchText);
        }

        void PickRandomSearchTerm()
        {
            var items = new[] { "MvvmCross", "wpdev", "MonoTouch", "Xamarin", "mvvm", "kittens" };

            var originalText = SearchText;
            var newText = originalText;
            while (originalText == newText)
            {
                var which = _rnd.Next(items.Length);
                newText = items[which];
            }
            SearchText = newText;
        }
    }
}
