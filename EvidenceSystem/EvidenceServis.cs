using System;
using System.Threading.Tasks;
using EvidenceSystem.Data;

namespace EvidenceSystem.Business
{
    public class EvidenceService : EvidenceServiceBase
    {
        public EvidenceService() : base(new EvidenceDbContext()) { }

        public override async Task AddEvidenceAsync(string name, string description)
        {
            if (description?.Length > 500)
                throw new ArgumentException("Описание слишком длинное (максимум 500 символов).");

            await base.AddEvidenceAsync(name, description);
        }
    }
}