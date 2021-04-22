namespace PMG.Services.Message
{
    using Domain.Home;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task CreateAsync(Messages message);
        Task RemoveAsync(Messages message);
        Task<IEnumerable<Messages>> GetAllAsync();
        Task<Messages> GetByIdAsync(string id);
        Task RemoveByIdAsync(string id);
    }
}