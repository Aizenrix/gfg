using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvidenceSystem.Data;
using EvidenceSystem.Models;

namespace EvidenceSystem.Business
{
    public abstract class EvidenceServiceBase : IEvidenceService
    {
        protected readonly EvidenceDbContext _dbContext;

        protected EvidenceServiceBase(EvidenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<List<Evidence>> GetAllEvidenceAsync()
        {
            return await _dbContext.GetAllAsync();
        }

        public virtual async Task<Evidence?> GetByIdAsync(int id)
        {
            return await _dbContext.GetByIdAsync(id);
        }

        public virtual async Task AddEvidenceAsync(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название не может быть пустым.");

            var evidence = new Evidence { Name = name.Trim(), Description = description?.Trim() ?? "" };
            await _dbContext.AddAsync(evidence);
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            return await _dbContext.DeleteAsync(id);
        }
    }
}