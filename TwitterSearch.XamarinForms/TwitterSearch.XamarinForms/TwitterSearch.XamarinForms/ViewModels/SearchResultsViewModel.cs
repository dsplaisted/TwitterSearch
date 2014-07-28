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
using TwitterSearch.Portable.Models;

namespace TwitterSearch.Portable.ViewModels
{
    public class SearchResultsViewModel : BaseViewModel
    {
        TwitterAuthenticator _twitterAuthenticator;
        IAuthInformation _authInformation;
        const int RESULTS_TO_SHOW = 100;

   

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
