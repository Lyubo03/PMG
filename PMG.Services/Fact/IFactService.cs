namespace PMG.Services.Fact
{
    using PMG.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFactService
    {
        Task<IEnumerable<Fact>> GetPhilosophyFactsAsync();
        Task<IEnumerable<Fact>> GetMathematicsFactsAsync();
        Task<IEnumerable<Fact>> GetEnglishFactsAsync();
        Task<IEnumerable<Fact>> GetPhysicsFactsAsync();
    }
}