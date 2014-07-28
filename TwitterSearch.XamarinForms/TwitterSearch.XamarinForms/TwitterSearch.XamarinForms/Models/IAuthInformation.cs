using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Models
{
    public interface IAuthInformation
    {
        string TwitterConsumerKey { get; }
        string TwitterConsumerSecret { get; }

        string AccessToken { get; }
        string AccessTokenSecret { get; }
    }
}
