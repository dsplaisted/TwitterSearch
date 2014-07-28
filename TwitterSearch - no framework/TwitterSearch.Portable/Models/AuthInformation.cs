using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Portable.Models
{
    class AuthInformation : IAuthInformation
    {
        #region Seekrit
        public string TwitterConsumerKey { get { return "8lPX03rNcbRpokKZXpmrA"; } }
        public string TwitterConsumerSecret { get { return "mY0rpwJ2KLBsUJoq5j0LuC75O045ZbD1dn1cQafrk";}}

        #region SUPER SEEKRIT
        public string AccessToken { get { return "14186951-rEmTxgNzLPzhHFC7CVWZc9HXELB8cnWTQU7BBJloz"; } }
        public string AccessTokenSecret { get { return "9gcOXiPG95ASBqPLANzfob6JU0cjKPkFYQdnDdem6FkQY"; } }
        #endregion
        #endregion
    }
}
