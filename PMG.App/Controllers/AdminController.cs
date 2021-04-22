namespace PMG.App.Controllers
{
    using PMG.Domain.Home;
    using Microsoft.AspNetCore.Mvc;
    using Models.Admin;
    using System;
    using Microsoft.AspNetCore.Authorization;
    using PMG.Services.Message;
    using System.Threading.Tasks;

    public class AdminController : Controller
    {
        private readonly IMessageService messageService;

        public AdminController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet("Admin/Create")]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost("Admin/Create")]
        public async Task<IActionResult> Create(MessageCreateBindingModel model)
        {
            Messages message = new Messages
            {
                Content = model.Content,
                PublishedOn = DateTime.UtcNow
            };

            await messageService.CreateAsync(message);

            return this.Redirect("/Home/Index");
        }

        [HttpGet("Home/Delete/{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await messageService.RemoveByIdAsync(id);
            return this.Redirect("/Home/Index");
        }
    }
}