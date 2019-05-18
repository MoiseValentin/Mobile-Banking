using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PCLCrypto;
using static PCLCrypto.WinRTCrypto;

namespace MobileBanking
{
    class PCLCrypto
    {
        public static string GenerateHash(string input)
        {
            var mac = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
            var keyMaterial = CryptographicBuffer.ConvertStringToBinary("securitate", Encoding.UTF8);
            var cryptoKey = mac.CreateKey(keyMaterial);
            var hash = CryptographicEngine.Sign(cryptoKey, CryptographicBuffer.ConvertStringToBinary(input, Encoding.UTF8));
            return CryptographicBuffer.EncodeToBase64String(hash);
        }
    }
}