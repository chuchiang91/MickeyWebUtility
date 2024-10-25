using MickeyWebUtility.Models.Shared;
using MickeyWebUtility.Models;

namespace MickeyWebUtility.Interfaces
{
    public interface ISGItineraryService
    {
        Task<Dictionary<string, (string Date, List<Itinerary> Items)>> GetSingaporeItinerary(string accessKey);
        Task<List<KeyListItem>> GetAvailableKeys();
    }
}
