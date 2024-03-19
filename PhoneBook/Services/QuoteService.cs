using Newtonsoft.Json.Linq;

namespace PhoneBook.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly HttpClient _httpClient;

        public QuoteService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<string?> GetRandomQuote()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://quotes.rest/qod?language=en");

            if (response.IsSuccessStatusCode)
            {
                var quoteResponse = await response.Content.ReadAsStringAsync();
                var quote = (string)((JObject?)JObject.Parse(quoteResponse))["contents"]["quotes"][0]["quote"];
                return quote;
            }
            else
            {
                throw new HttpRequestException($"Error while fetching the quote. Status code: {response.StatusCode}");
            }
        }
    }
}