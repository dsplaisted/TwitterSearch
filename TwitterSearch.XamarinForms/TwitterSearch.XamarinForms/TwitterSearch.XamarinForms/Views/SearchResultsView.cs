using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TwitterSearch.Views
{
    class SearchResultsView : ContentPage
    {
        public SearchResultsView()
        {
            Title = "TwitterSearch Results";

            var listView = new ListView
            {
                RowHeight = 80
            };
            listView.SetBinding(ListView.ItemsSourceProperty, "Tweets");
            listView.ItemTemplate = new DataTemplate(typeof(TweetCell));

            Content = listView;
        }

        class TweetCell : ViewCell
        {
            public TweetCell()
            {
                var image = new Image
                {
                    HorizontalOptions = LayoutOptions.Start
                };
                image.SetBinding(Image.SourceProperty, new Binding("ProfileImageUrl"));
                image.WidthRequest = image.HeightRequest = 40;

                var user = new Label()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                user.SetBinding(Label.TextProperty, "Author");

                var tweetText = new Label()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                tweetText.SetBinding(Label.TextProperty, "Text");

                var textLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    Children = { user, tweetText }
                };

                var viewLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { image, textLayout },
                    Padding = new Thickness(0, 5)
                };

                this.View = viewLayout;

            }
        }
    }
}
