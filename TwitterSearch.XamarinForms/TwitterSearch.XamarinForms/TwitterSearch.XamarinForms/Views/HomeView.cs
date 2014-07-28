using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TwitterSearch.Views
{
    class HomeView : ContentPage
    {
        Entry _searchText;
        Button _searchButton;
        Button _randomButton;

        public HomeView()
        {
            _searchText = new Entry() { Placeholder = "Search for..." };
            _searchText.SetBinding(Entry.TextProperty, "SearchText");

            _searchButton = new Button() { Text = "Search" };
            _searchButton.SetBinding(Button.CommandProperty, "SearchCommand");

            _randomButton = new Button() { Text = "Random" };
            _randomButton.SetBinding(Button.CommandProperty, "PickRandomCommand");

            Content = new StackLayout()
            {
                
                Children =
                {
                    _searchText,
                    _searchButton,
                    _randomButton
                }
            };
        }
    }
}
