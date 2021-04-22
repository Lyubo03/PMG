namespace PMG.Services.Message
{
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using PMG.Domain.Home;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MessageService : IMessageService
    {
        private readonly PMGDbContext context;
        public MessageService(PMGDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Messages message)
        {
            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Messages>> GetAllAsync()
        {
            var messages = await context.Messages.ToListAsync();
            return messages;
        }

        public async Task<Messages> GetByIdAsync(string id)
        {
            var message = await context.Messages.SingleOrDefaultAsync(x => x.Id == id);
            return message;
        }

        public async Task RemoveAsync(Messages message)
        {
            context.Messages.Remove(message);
            await context.SaveChangesAsync();
        }

        public async Task RemoveByIdAsync(string id)
        {
            var message = GetByIdAsync(id).GetAwaiter().GetResult();
            RemoveAsync(message).GetAwaiter().GetResult();
        }
    }
}