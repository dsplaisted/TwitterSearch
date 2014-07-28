using Budgie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace TwitterSearch.WindowsStore.Models
{
    public class TwitterPlatformAdapter_WindowsStore : IPlatformAdaptor
    {
        public string ComputeSha1Hash(string key, string buffer)
        {
            var algorithm = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            var keymaterial = CryptographicBuffer.ConvertStringToBinary(key, BinaryStringEncoding.Utf8);
            var hmacKey = algorithm.CreateKey(keymaterial);

            return CryptographicBuffer.EncodeToBase64String(CryptographicEngine.Sign(
                hmacKey,
                CryptographicBuffer.ConvertStringToBinary(buffer, BinaryStringEncoding.Utf8)));
        }
    }
}
