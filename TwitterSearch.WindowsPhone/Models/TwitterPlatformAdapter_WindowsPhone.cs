using Budgie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.WindowsPhone.Models
{
    public class TwitterPlatformAdapter_WindowsPhone : IPlatformAdaptor
    {
        public string ComputeSha1Hash(string key, string buffer)
        {
            using (var hasher = new HMACSHA1(UTF8Encoding.UTF8.GetBytes(key)))
            {
                return Convert.ToBase64String(hasher.ComputeHash(UTF8Encoding.UTF8.GetBytes(buffer)));
            }
        }
    }
}
