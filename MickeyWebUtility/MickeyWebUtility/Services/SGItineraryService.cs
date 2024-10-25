using System.Net.Http.Json;
using MickeyWebUtility.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MickeyWebUtility.Models.Shared;
using MickeyWebUtility.Interfaces;

namespace MickeyWebUtility.Services
{
 

    public class SGItineraryService : ISGItineraryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SGItineraryService> _logger;
        private readonly IMasterKeyService _masterKeyService;
        private readonly string _apiKey;
        private readonly string _range = "Sheet1!A:F";

        public SGItineraryService(
            HttpClient httpClient,
            ILogger<SGItineraryService> logger,
            IMasterKeyService masterKeyService,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _masterKeyService = masterKeyService;
            _apiKey = configuration["GoogleSheets:ApiKey"];
        }

        public async Task<List<KeyListItem>> GetAvailableKeys()
        {
            try
            {
                var masterKeys = await _masterKeyService.GetAllMasterKeys();
                return masterKeys
                    .Where(mk => mk.Service == "SGItinerary")
                    .Select(mk => new KeyListItem { Key = mk.Key, Description = mk.Key })
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting available keys");
                throw;
            }
        }

        public async Task<Dictionary<string, (string Date, List<Itinerary> Items)>> GetSingaporeItinerary(string accessKey)
        {
            try
            {
                if (string.IsNullOrEmpty(accessKey))
                {
                    throw new ArgumentException("Access key is required");
                }

                // Get spreadsheet ID from master key service
                var masterKey = await _masterKeyService.GetMasterKeyByKey(accessKey);
                if (masterKey == null || masterKey.Service != "SGItinerary")
                {
                    throw new UnauthorizedAccessException("Invalid access key");
                }

                var url = $"https://sheets.googleapis.com/v4/spreadsheets/{masterKey.SpreadsheetId}/values/{_range}?key={_apiKey}";
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