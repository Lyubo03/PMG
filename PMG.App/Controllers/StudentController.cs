namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    using Services.Bookmark;
    using PMG.Domain.SchoolSubjects;
    using System.Threading.Tasks;

    public class StudentController : Controller
    {
        private readonly IBookmarkService bookmarkService;

        public StudentController(IBookmarkService bookmarkService)
        {
            this.bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> Bookmark()
        {
            var marks = new Dictionary<string, IEnumerable<ISubject>>();
            var bookmark = await bookmarkService.GetBookmarkByUsernameAsync(this.User.Identity.Name);

            ViewData["StudentUsername"] = this.User.Identity.Name;

            var philosophy = bookmark.PhilosophyMarks.ToList();

            var english = bookmark.EnglishMarks.ToList();

            var mathematics = bookmark.MathematicsMarks.ToList();

            marks["Philosophy"] = philosophy;
            marks["Mathematics"] = mathematics;
            marks["English"] = english;

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            return this.View(marks);
        }
    }
}