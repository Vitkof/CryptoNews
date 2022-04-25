using System;

namespace CryptoNews.Utilities
{
    public class CaptchaResult
    {
        public string CaptchaCode { get; set; }
        public byte[] CaptchaBytes { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CaptchaBase64 => Convert.ToBase64String(CaptchaBytes);
    }
}
