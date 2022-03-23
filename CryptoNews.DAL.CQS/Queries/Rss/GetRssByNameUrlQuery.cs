using CryptoNews.Core.DTO;


namespace CryptoNews.DAL.CQS.Queries.Rss
{
    public class GetRssByNameUrlQuery : IQuery<RssSourceDto>
    {
        public string Name { get; set; }
        public string Url { get; set; }


        public GetRssByNameUrlQuery(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
