using System.Collections.Generic;
using System.Threading.Tasks;
using EvidenceSystem.Models;

namespace EvidenceSystem.Business
{
    public interface IEvidenceService
    {
        Task<List<Evidence>> GetAllEvidenceAsync();
        Task<Evidence?> GetByIdAsync(int id);
        Task AddEvidenceAsync(string name, string description);
        Task<bool> DeleteByIdAsync(int id);
    }
}