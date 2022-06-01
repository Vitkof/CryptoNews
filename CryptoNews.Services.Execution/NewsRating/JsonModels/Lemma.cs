using System.Diagnostics.CodeAnalysis;

namespace CryptoNews.Services.Implement.NewsRating.JsonModels
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class Lemma
    {
        public int start { private get; set; }
        public int end { private get; set; }
        public string value { get; set; }
    }
}
