using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.Services.Implement.NewsRating.JsonModels;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoNews.Services.Implement.NewsRating
{
    public class NewsRatingService : INewsRatingService
    {
        public async Task<IEnumerable<NewsDto>> Rating(IEnumerable<NewsDto> newsWithoutRating)
        {
            try
            {
                var newsWithRating = new List<NewsDto>();

                var afinnEN = await GetAfinnWords("en");
                var afinnRU = await GetAfinnWords("ru");

                foreach (var newsDto in newsWithoutRating)
                {
                    var body = newsDto.Body;

                    body = ClearBody(body);

                    var responseNews = await Response(body);

                    if (responseNews == null)
                    {
                        Log.Error($" Response news is unsucceeded {newsDto.Url}");
                        continue;
                    }

                    var rating = RatingNews(afinnEN, responseNews);

                    if (rating == null)
                    {
                        rating = RatingNews(afinnRU, responseNews);
                    }

                    if (rating != null)
                    {
                        newsDto.Rating = (float)rating;
                    }
                    
                    newsWithRating.Add(newsDto);
                }

                return newsWithRating;
            }
            catch (Exception ex)
            {
                Log.Error($"Error Rate News: {ex.Message}");
                return default;
            }
        }

        private static float? RatingNews(Dictionary<string, sbyte> afinn, IEnumerable<string> words)
        {
            try
            {
                var count = 0;
                var rate = 0;
                foreach (var word in words)
                {
                    foreach (var pair in afinn.Where(pairs => pairs.Key.Equals(word)))
                    {
                        count++;
                        rate += pair.Value;
                    }
                }

                if (count == 0)
                {
                    return null;
                }
                var rating = (float)rate / count * 100;

                return rating;
            }
            catch (Exception ex)
            {
                Log.Error($"Error rating news return null {ex}");
                return null;
            }
        }

        private static async Task<Dictionary<string, sbyte>> GetAfinnWords(string lang)
        {
            var allText = await File.ReadAllTextAsync(
                $"../CryptoNews.Services.Execution/NewsRating/Afinn/AFINN-{lang}.json");
            var dict = JsonConvert.DeserializeObject<Dictionary<string, sbyte>>(allText);

            return dict;
        }

        private static async Task<List<string>> Response(string body)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var url =
                    "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=4fb10dbc749498f4b6e0f07fc49a43d7f023138d";

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent("[{\"text\":\"" + body + "\"}]",
                        Encoding.UTF8,
                        "application/json")
                };
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                var arrayDeserialized = JsonConvert.DeserializeObject<Root[]>(responseContent);

                if (arrayDeserialized == null)
                {
                    Log.Error($"Error NewsResponse return null {body} ");
                    return new List<string> { null };
                }

                var lemmaList = arrayDeserialized[0].annotations.lemma;
                var words = lemmaList.Select(model => model.value).ToList();

                return words;
            }
            catch (Exception e)
            {
                Log.Error($"This Body is invalid {e.Message}. {body}");
                return new List<string>() { null };
            }
        }

        private static string ClearBody(string body)
        {
            var clearBody = @"<p>(.*?)<\/p>";
            var clearedBody = Regex.Matches(body, clearBody);
            var aggregate = clearedBody.Aggregate("", (current, n) => current + $"{n} ");
            if (string.IsNullOrEmpty(aggregate))
            {
                var clear = Regex.Replace(body, @"(<.*?>)|(http.*?\s)|(\n\t)", " ");
                char[] ch = { '.', '!', '?', '"', '\r' };
                var split = clear.Split(ch, StringSplitOptions.RemoveEmptyEntries);
                var agg = split.Aggregate("", (current, n) => current + $"{n.Trim()} ");
                return agg;
            }
            var clearMarkup = Regex.Replace(aggregate, @"(<.*?>)", " ");

            return clearMarkup;
        }
    }
}
