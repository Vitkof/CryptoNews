using System.Diagnostics.CodeAnalysis;

namespace CryptoNews.Services.Implement.NewsRating.JsonModels
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class Root
    {
        public string text { get; set; }
        public Annotations annotations { get; set; }
    }
}
