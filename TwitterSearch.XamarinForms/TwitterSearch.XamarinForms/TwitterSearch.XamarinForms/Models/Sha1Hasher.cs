using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.Models
{
    class Sha1Hasher : Budgie.IPlatformAdaptor
    {
        public string ComputeSha1Hash(string key, string buffer)
        {

            var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
            //CryptographicHash hasher = algorithm.CreateHash(UTF8Encoding.UTF8.GetBytes(key));
            var keyMaterial = WinRTCrypto.CryptographicBuffer.ConvertStringToBinary(key, Encoding.UTF8);
            var hmacKey = algorithm.CreateKey(keyMaterial);

            return WinRTCrypto.CryptographicBuffer.EncodeToBase64String(WinRTCrypto.CryptographicEngine.Sign(
                hmacKey,
                WinRTCrypto.CryptographicBuffer.ConvertStringToBinary(buffer, Encoding.UTF8)));

        }
    }
}
