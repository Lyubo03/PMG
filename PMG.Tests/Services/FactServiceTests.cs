namespace PMG.Tests.Services
{
    using Data;
    using PMG.Domain;
    using PMG.Domain.Facts;
    using PMG.Services.Fact;
    using System.Linq;
    using System.Threading.Tasks;
    using Tests.Factory;
    using Xunit;

    public class FactServiceTests
    {
        [Fact]
        private async Task TestGetPhilosophyFacts_WithTestData_ShouldReturnContent()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var factService = new FactService(context);
            await SeedFacts(context);

            var philosophyFact = (await factService.GetPhilosophyFactsAsync()).FirstOrDefault();
            var expectedContent = "Който не обича самотата не обича и свободата";
            var expectedAuthor = "Шопенхауер";

            var actualContent = philosophyFact.Content;
            var actualAuthor = philosophyFact.Author;

            Assert.Equal(expectedContent, actualContent);
            Assert.Equal(expectedAuthor, actualAuthor);
        }
        [Fact]
        private async Task TestGetPhilosophyFacts_WithTestData_ShouldReturnNull()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var factService = new FactService(context);

            var philosophyFacts = (await factService.GetPhilosophyFactsAsync()).FirstOrDefault();

            Assert.True(philosophyFacts == null, "Method GetPhilosophyFacts() does not work correctly");
        }
        [Fact]
        private async Task TestGetMathemtaticsFacts_WithTestData_ShouldReturnNull()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var factService = new FactService(context);

            var philosophyFacts = (await factService.GetMathematicsFactsAsync()).FirstOrDefault();

            Assert.True(philosophyFacts == null, "Method GetMathematicsFacts() does not work correctly");
        }
        [Fact]
        private async Task TestGetPhysicsFacts_WithTestData_ShouldReturnNull()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var factService = new FactService(context);

            var philosophyFacts = (await factService.GetPhysicsFactsAsync()).FirstOrDefault();

            Assert.True(philosophyFacts == null, "Method GetPhysicsFacts() does not work correctly");
        }
        [Fact]
        private async Task TestGetEnglishFacts_WithTestData_ShouldReturnNull()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var factService = new FactService(context);

            var philosophyFacts = (await factService.GetEnglishFactsAsync()).FirstOrDefault();

            Assert.True(philosophyFacts == null, "Method GetEnglishFacts() does not work correctly");
        }
        public async Task SeedFacts(PMGDbContext context)
        {
            var philosophyFact = new PhilosophySages
            {
                Content = "Който не обича самотата не обича и свободата",
                Author = "Шопенхауер"
            };
            var mathematics = new MathematicsSentences
            {
                Content = "Math is life",
                Author = "Somebody"
            };
            var physics = new PhysicsFacts
            {
                Content = "Physics is beautiful",
                Author = "Man"
            };
            var english = new EnglishSlangs
            {
                Content = "To turn into peaces",
                Author = "Break"
            };

            context.EnglishSlangs.Add(english);
            context.PhysicsFacts.Add(physics);
            context.MathematicsSentences.Add(mathematics);
            context.PhilosophySages.Add(philosophyFact);

            await context.SaveChangesAsync();
        }

        [Fact]
        private async Task TestGetMathematicsFact_WithTestData_ShouldReturnContent()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var factService = new FactService(context);
            await SeedFacts(context);

            var mathematicsFact = (await factService.GetMathematicsFactsAsync()).FirstOrDefault();
            var expectedContent = "Math is life";
            var expectedAuthor = "Somebody";

            var actualContent = mathematicsFact.Content;
            var actualAuthor = mathematicsFact.Author;

            Assert.Equal(expectedContent, actualContent);
            Assert.Equal(expectedAuthor, actualAuthor);
        }
        [Fact]
        private async Task TestGetPhysicsFact_WithTestData_ShouldReturnContent()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var factService = new FactService(context);
            await SeedFacts(context);

            var physicsFact = (await factService.GetPhysicsFactsAsync()).FirstOrDefault();
            var expectedContent = "Physics is beautiful";
            var expectedAuthor = "Man";

            var actualContent = physicsFact.Content;
            var actualAuthor = physicsFact.Author;

            Assert.Equal(expectedContent, actualContent);
            Assert.Equal(expectedAuthor, actualAuthor);
        }
        [Fact]
        private async Task TestGetEnglishFact_WithTestData_ShouldReturnContent()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var factService = new FactService(context);
            await SeedFacts(context);

            var englishSlang = (await factService.GetEnglishFactsAsync()).FirstOrDefault();
            var expectedContent = "To turn into peaces";
            var expectedAuthor = "Break";

            var actualContent = englishSlang.Content;
            var actualAuthor = englishSlang.Author;

            Assert.Equal(expectedContent, actualContent);
            Assert.Equal(expectedAuthor, actualAuthor);
        }
    }
}