﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitterSearch.Infrastructure;
using Xamarin.Forms;

namespace TwitterSearch.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        Random _rnd = new Random();
        readonly INavigationService _navigationService;

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            //PickRandomSearchTerm();
            SearchText = "#MVPSummit";
        }

        string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        Command _searchCommand;
        public ICommand SearchCommand
        {
            get { return _searchCommand ?? (_searchCommand = new Command(Search)); }
        }

        Command _pickRandomCommand;
        public ICommand PickRandomCommand
        {
            get { return _pickRandomCommand ?? (_pickRandomCommand = new Command(PickRandomSearchTerm)); }
        }

        void Search()
        {
            Debug.WriteLine("Searching...");
            Task ignore =_navigationService.NavigateToAsync<SearchResultsViewModel>(SearchText);
        }

        void PickRandomSearchTerm()
        {
            var items = new[] { "#MVPSummit", "MvvmCross", "Xamarin", "mvvm", "kittens" };

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
