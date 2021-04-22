namespace PMG.Data.Seeders
{
    using PMG.Domain.Facts;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class PhilosophySeeder : ISeeder
    {
        private readonly PMGDbContext context;

        public PhilosophySeeder(PMGDbContext context)
        {
            this.context = context;
        }
        public async Task SeedAsync()
        {
            if (!this.context.PhilosophySages.Any())
            {
                var data = File.ReadAllText(@"D:\PMG\PMG.Data\Philosophy.txt").Split(new[] { '\n', '-' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                for (int i = 0; i < data.Count - 1; i += 2)
                {
                    var content = data[i];
                    var author = data[i + 1];

                    var fact = new PhilosophySages
                    {
                        Content = content,
                        Author = author
                    };

                    await context.PhilosophySages.AddAsync(fact);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}