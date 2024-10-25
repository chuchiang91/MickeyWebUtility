using MickeyWebUtility.Models.Shared;

namespace MickeyWebUtility.Interfaces
{
    public interface IMasterKeyService
    {
        Task<MasterKey> GetMasterKeyByKey(string key);
        Task<List<MasterKey>> GetAllMasterKeys();
        Task<bool> ValidateKey(string key, string service);
    }
}
