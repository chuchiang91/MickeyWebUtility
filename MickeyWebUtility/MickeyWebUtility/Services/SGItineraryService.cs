using System.Net.Http.Json;
using MickeyWebUtility.Models;
using Microsoft.Extensions.Logging;

namespace MickeyWebUtility.Services
{
    public class SGItineraryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SGItineraryService> _logger;
        private readonly string _spreadsheetId = "1myB46iSQ25rKP78f-eJpl7juv_q18PKTi-JWo6W6jpo";
        private readonly string _apiKey = "AIzaSyB45KFkcg2vc1St49hoKJ8B9yp_VJpG0AY";
        private readonly string _range = "Sheet1!A:F";

        public SGItineraryService(HttpClient httpClient, ILogger<SGItineraryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Dictionary<string, (string Date, List<Itinerary> Items)>> GetSingaporeItinerary()
        {
            try
            {
                var url = $"https://sheets.googleapis.com/v4/spreadsheets/{_spreadsheetId}/values/{_range}?key={_apiKey}";
                var response = await _httpClient.GetFromJsonAsync<SheetResponse>(url);
                return ParseSheetResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetSingaporeItinerary");
                throw;
            }
        }

        private Dictionary<string, (string Date, List<Itinerary> Items)> ParseSheetResponse(SheetResponse response)
        {
            var itinerary = new Dictionary<string, (string Date, List<Itinerary> Items)>();

            if (response?.Values != null && response.Values.Count > 1)
            {
                foreach (var row in response.Values.Skip(1)) // Skip header row
                {
                    var day = row[0].ToString();
                    var date = row[1].ToString();

                    if (!itinerary.ContainsKey(day))
                    {
                        itinerary[day] = (date, new List<Itinerary>());
                    }

                    itinerary[day].Items.Add(new Itinerary
                    {
                        Time = row[2].ToString(),
                        Activity = row[3].ToString(),
                        Icon = row[4].ToString(),
                        Location = row.Count > 5 ? row[5].ToString() : ""
                    });
                }
            }

            return itinerary;
        }
    }

    public class SheetResponse
    {
        public List<List<string>> Values { get; set; }
    }
}