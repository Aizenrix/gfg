using System.Collections.Generic;
using System.Threading.Tasks;
using EvidenceSystem.Data;
using EvidenceSystem.Models;

namespace EvidenceSystem
{
    public class EvidenceService
    {
        private readonly EvidenceDbContext _dbContext = new();

        public async Task<List<Evidence>> GetAllEvidenceAsync() => await _dbContext.GetAllAsync();

        public async Task<Evidence?> GetByIdAsync(int id) => await _dbContext.GetByIdAsync(id);

        public async Task AddEvidenceAsync(string name, string description)
        {
            var evidence = new Evidence { Name = name, Description = description };
            await _dbContext.AddAsync(evidence);
        }

        public async Task<bool> DeleteByIdAsync(int id) => await _dbContext.DeleteAsync(id);
    }
}