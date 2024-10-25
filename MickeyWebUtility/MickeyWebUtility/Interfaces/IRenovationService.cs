using MickeyWebUtility.Models.Shared;
using MickeyWebUtility.Services;

namespace MickeyWebUtility.Interfaces
{
    public interface IRenovationService
    {
        Task<List<RenovationItem>> GetRenovationItems(string accessKey);
        Task<List<KeyListItem>> GetAvailableKeys();
    }
}
