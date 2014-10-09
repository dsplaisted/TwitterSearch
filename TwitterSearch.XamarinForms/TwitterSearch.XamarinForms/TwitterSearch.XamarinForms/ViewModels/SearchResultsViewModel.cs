using Budgie;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwitterSearch.Infrastructure;
using TwitterSearch.Models;

namespace TwitterSearch.ViewModels
{
    public class SearchResultsViewModel : BaseViewModel
    {
        TwitterAuthenticator _twitterAuthenticator;
        IAuthInformation _authInformation;
        const int RESULTS_TO_SHOW = 10;

   

        public SearchResultsViewModel(INavigationService navigationService,
            IPlatformAdaptor platformAdapter, IAuthInformation authInformation)
        {

            _authInformation = authInformation;
            _twitterAuthenticator = new TwitterAuthenticator(platformAdapter, _authInformation.TwitterConsumerKey, _authInformation.TwitterConsumerSecret);
            _isSearching = true;
            Tweets = new ObservableCollection<Tweet>();
        }

        public override async void Navigated(string navigationParameter)
        {
            try
            {
                IsSearching = true;
                Tweets.Clear();

                var clientResponse = await _twitterAuthenticator.AuthenticateAsync(_authInformation.AccessToken, _authInformation.AccessTokenSecret);
                if (clientResponse.StatusCode != HttpStatusCode.OK)
                {
                    Debug.WriteLine("Error authenticating: " + clientResponse.StatusCode + ": " + clientResponse.ErrorMessage);
                    return;
                }
                TwitterClient client = clientResponse.Content;

                var searchResponse = await client.SearchAsync(navigationParameter, RESULTS_TO_SHOW);
                if (searchResponse.StatusCode != HttpStatusCode.OK)
                {
                    Debug.WriteLine("Error searching: " + clientResponse.StatusCode + ": " + clientResponse.ErrorMessage);
                    return;
                }

                Tweets.Clear();

                //Tweets.Add(new Tweet()
                //{
                //    Author = "dsplaisted",
                //    ProfileImageUrl = new Uri("https://pbs.twimg.com/profile_images/51934680/iguana_bigger.jpg"),
                //    Text = "Short tweet",
                //    Timestamp = DateTime.Now
                //});

                //Tweets.Add(new Tweet()
                //{
                //    Author = "dsplaisted",
                //    ProfileImageUrl = new Uri("https://pbs.twimg.com/profile_images/51934680/iguana_bigger.jpg"),
                //    Text = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                //    Timestamp = DateTime.Now
                //});

                foreach (var tweet in searchResponse.Content)
                {
                    Tweets.Add(new Tweet()
                        {
                            Author = tweet.User.ScreenName,
                            ProfileImageUrl = tweet.User.ProfileImageUri,
                            Text = tweet.Text,
                            Timestamp = tweet.CreatedAt
                        });
                }

                IsSearching = false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error searching Twitter: " + ex.ToString());
            }
        }

        bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { SetProperty(ref _isSearching, value); }
        }

        Tweet _selectedTweet;
        public Tweet SelectedTweet
        {
            get { return _selectedTweet; }
            set { SetProperty(ref _selectedTweet, value); }
        }

        public ObservableCollection<Tweet> Tweets { get; private set; }
        
    }
}
