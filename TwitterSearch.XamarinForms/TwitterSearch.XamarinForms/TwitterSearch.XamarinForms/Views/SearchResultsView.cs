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
                //RowHeight = 80
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


                //var image = new BoxView()
                //{
                //    HorizontalOptions = LayoutOptions.Start,
                //    VerticalOptions = LayoutOptions.Start,
                //    BackgroundColor = Color.Red
                //};
                //image.WidthRequest = image.HeightRequest = 40;

                //var imageBorder = new Frame()
                //{
                //    Content = image,
                //    //Content = textLayout,
                //    OutlineColor = Color.Red
                //};

                var user = new Label()
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Font = Font.SystemFontOfSize(NamedSize.Large)
                };
                user.SetBinding(Label.TextProperty, "Author");

                var tweetText = new Label()
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    //HeightRequest = 100
                };
                tweetText.SetBinding(Label.TextProperty, "Text");

                var textLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Children = { user, tweetText }
                };

                //var horizontal = new StackLayout()
                //{
                //    Orientation = StackOrientation.Horizontal,
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.StartAndExpand,
                //    //MinimumHeightRequest = 100,
                //    //MinimumWidthRequest = 100,
                //    Children = { image, textLayout },
                //    Padding = new Thickness(0, 5)
                //};

                //var textLayout2 = new StackLayout()
                //{
                //    Orientation = StackOrientation.Vertical,
                //    HorizontalOptions = LayoutOptions.StartAndExpand,
                //    VerticalOptions = LayoutOptions.StartAndExpand,
                //    Children = { image, textLayout }
                //};

                

                var viewLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    //HorizontalOptions = LayoutOptions.StartAndExpand,
                    Children = { image, textLayout},
                    Padding = new Thickness(0, 5)
                };

                this.View = viewLayout;

            }
        }
    }
}
