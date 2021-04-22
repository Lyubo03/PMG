namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PMG.App.Models.Facts;
    using PMG.Services.Fact;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FactsController : Controller
    {
        private readonly IFactService factService;
        private Random random;

        public FactsController(IFactService factService)
        {
            this.factService = factService;
            random = new Random();
        }

        [HttpGet]
        public async Task<IActionResult> Facts()
        {
            var facts = new Dictionary<string, IFacts>();


            facts["Philosophy"] = await GetPhilosophyFactAsync();

            facts["English"] = await GetEnglishFactAsync();

            facts["Physics"] = await GetPhysicsFactAsync();

            facts["Mathematics"] = await GetMathematicsFactAsync();

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return View(facts);
        }

        private async Task<IFacts> GetPhilosophyFactAsync()
        {
            var philosophy = (await factService.GetPhilosophyFactsAsync())
               .Select(f => new FactBindingModel
               {
                   Content = f.Content,
                   Author = f.Author
               })
               .ToList();

            var index = random.Next(1, philosophy.Count);

            return philosophy[index];
        }
        private async Task<IFacts> GetEnglishFactAsync()
        {
            var english = (await factService.GetEnglishFactsAsync())
               .Select(f => new FactBindingModel
               {
                   Content = f.Content,
                   Author = f.Author
               })
               .ToList();

            var index = random.Next(1, english.Count);

            return english[index];
        }
        private async Task<IFacts> GetPhysicsFactAsync()
        {
            var physics = (await factService.GetPhysicsFactsAsync())
               .Select(f => new FactBindingModel
               {
                   Content = f.Content,
                   Author = f.Author
               })
               .ToList();

            var index = random.Next(1, physics.Count);

            return physics[index];
        }
        private async Task<IFacts> GetMathematicsFactAsync()
        {
            var mathematics = (await factService.GetMathematicsFactsAsync())
               .Select(f => new FactBindingModel
               {
                   Content = f.Content,
                   Author = f.Author
               })
               .ToList();

            var index = random.Next(1, mathematics.Count);

            return mathematics[index];
        }
    }
}