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
                //RowHeight = 300,
                HorizontalOptions = LayoutOptions.Fill
            };

            listView.HasUnevenRows = true;
            listView.ItemTemplate = new DataTemplate(typeof(TweetCell));
            listView.SetBinding(ListView.ItemsSourceProperty, "Tweets");

            Content = listView;
        }

        class TweetCell : ViewCell
        {
            public TweetCell()
            {
                var image = new Image
                {
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                image.SetBinding(Image.SourceProperty, new Binding("ProfileImageUrl"));
                image.WidthRequest = image.HeightRequest = 40;

                var user = new Label()
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Start,
                    Font = Font.SystemFontOfSize(NamedSize.Large)
                };
                user.SetBinding(Label.TextProperty, "Author");

                var tweetText = new Label()
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Start,
                };
                tweetText.SetBinding(Label.TextProperty, "Text");

                var textLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Start,
                    Children = { user, tweetText }
                };

                //var viewLayout = new StackLayout()
                //{
                //    Orientation = StackOrientation.Horizontal,
                //    HorizontalOptions = LayoutOptions.Fill,
                //    Children = { image, textLayout},
                //    Padding = new Thickness(0, 5),
                //    //WidthRequest = 200
                //};

                //var viewLayout = new RelativeLayout();
                //viewLayout.Children.Add(image, Constraint.Constant(0),Constraint.Constant(0))
                var viewLayout = new Grid()
                {
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = new GridLength(40)},
                        new ColumnDefinition { Width = GridLength.Auto}
                    },
                    RowDefinitions = { new RowDefinition { Height = GridLength.Auto} }
                };
                viewLayout.Children.Add(image, 0, 0);
                viewLayout.Children.Add(textLayout, 1, 0);

                this.View = viewLayout;

            }
        }
    }
}
