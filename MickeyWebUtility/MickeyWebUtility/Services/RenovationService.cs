using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MickeyWebUtility.Services
{
    public class RenovationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RenovationService> _logger;
        private readonly string _spreadsheetId = "";
        private readonly string _apiKey = "";
        private readonly string _range = "Sheet1!A:K";

        public RenovationService(HttpClient httpClient, ILogger<RenovationService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<RenovationItem>> GetRenovationItems()
        {
            try
            {
                var url = $"https://sheets.googleapis.com/v4/spreadsheets/{_spreadsheetId}/values/{_range}?key={_apiKey}";
                _logger.LogInformation($"Requesting data from URL: {url}");

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Received response: {jsonString}");

                var jsonDocument = JsonDocument.Parse(jsonString);
                var values = jsonDocument.RootElement.GetProperty("values").EnumerateArray().ToList();

                _logger.LogInformation($"Number of rows in response: {values.Count}");

                if (values.Count <= 1)
                {
                    _logger.LogWarning("No data rows found in the response");
                    return new List<RenovationItem>();
                }

                var items = new List<RenovationItem>();
                for (int i = 1; i < values.Count; i++) // Skip header row
                {
                    var row = values[i].EnumerateArray().Select(v => v.GetString()).ToList();
                    _logger.LogInformation($"Processing row {i}: {string.Join(", ", row)}");

                    if (row.Count >= 11)
                    {
                        var item = new RenovationItem
                        {
                            ItemName = row[0] ?? string.Empty,
                            Quantity = ParseDecimal(row[1]),
                            Measurement = row[2] ?? string.Empty,
                            UnitPrice = ParseDecimal(row[3]),
                            TotalPrice = ParseDecimal(row[4]),
                            PurchaseDate = ParseDateTime(row[5]),
                            ShopName = row[6] ?? string.Empty,
                            Salesperson = row[7] ?? string.Empty,
                            Contact = row[8] ?? string.Empty,
                            InvoiceQuotationNumber = row[9] ?? string.Empty,
                            Category = row[10] ?? string.Empty
                        };
                        items.Add(item);
                    }
                    else
                    {
                        _logger.LogWarning($"Row {i} does not have enough columns. Skipping.");
                    }
                }

                _logger.LogInformation($"Parsed {items.Count} renovation items");
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetRenovationItems");
                throw;
            }
        }

        private decimal ParseDecimal(string value)
        {
            if (decimal.TryParse(value, out decimal result))
                return result;

            _logger.LogWarning($"Failed to parse decimal value: {value}");
            return 0;
        }

        private DateTime? ParseDateTime(string value)
        {
            if (DateTime.TryParse(value, out DateTime result))
                return result;

            _logger.LogWarning($"Failed to parse date value: {value}");
            return null;
        }
    }

    public class RenovationItem
    {
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string Measurement { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string ShopName { get; set; }
        public string Salesperson { get; set; }
        public string Contact { get; set; }
        public string InvoiceQuotationNumber { get; set; }
        public string Category { get; set; }
    }
}