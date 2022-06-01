using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CryptoNews.Services.Implement.NewsRating.JsonModels
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public class Annotations
    {
        public IEnumerable<Lemma> lemma { get; set; }
    }
}
