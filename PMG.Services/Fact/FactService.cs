namespace PMG.Services.Fact
{
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using PMG.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class FactService : IFactService
    {
        private readonly PMGDbContext context;

        public FactService(PMGDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Fact>> GetPhilosophyFactsAsync()
        {
            var facts = await context.PhilosophySages.ToListAsync();
            return facts;
        }
        public async Task<IEnumerable<Fact>> GetMathematicsFactsAsync()
        {
            var facts = await context.MathematicsSentences.ToListAsync();
            return facts;
        }
        public async Task<IEnumerable<Fact>> GetEnglishFactsAsync()
        {
            var facts = await context.EnglishSlangs.ToListAsync();
            return facts;
        }
        public async Task<IEnumerable<Fact>> GetPhysicsFactsAsync()
        {
            var facts = await context.PhysicsFacts.ToListAsync();
            return facts;
        }
    }
}