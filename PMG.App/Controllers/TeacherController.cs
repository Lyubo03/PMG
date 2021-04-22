namespace PMG.App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PMG.App.Models.Teacher;
    using PMG.Domain.SchoolSubjects;
    using PMG.Services.Bookmark;
    using PMG.Services.Marks;
    using PMG.Services.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeacherController : Controller
    {
        private readonly IMarkService markService;
        private readonly IUserService userService;
        private readonly IBookmarkService bookmarkService;
        private static string studentUsername;

        public TeacherController(IMarkService markService,
            IUserService userService,
            IBookmarkService bookmarkService)
        {
            this.markService = markService;
            this.userService = userService;
            this.bookmarkService = bookmarkService;
        }

        [HttpGet("Teacher/Marks")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Marks()
        {
            ViewData["Students"] = (await userService.GetAllStudentsAsync())
                 .Select(x => x.UserName).ToList();

            return this.View();
        }

        [HttpPost("Teacher/Marks")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Marks(MarkBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect("Teacher/Marks");
            }

            var user = await userService.GetUserWithBookmarkByUsrenameAsync(model.StudentUsername);

            studentUsername = model.StudentUsername;
            ViewData["StudentUsername"] = model.StudentUsername;

            if (model.Subject == "Philosophy")
            {
                await markService.CreateMarkPhilosophyAsync(model.Mark, user);
            }

            else if (model.Subject == "Matematics")
            {
                await markService.CreateMarkMathematicsAsync(model.Mark, user);
            }

            else if (model.Subject == "English")
            {
                await markService.CreateMarkEnglishAsync(model.Mark, user);
            }

            return this.View();
        }

        //[HttpGet("Teacher/DeleteOrUpdate")]
        //[Authorize(Roles = "Teacher")]
        //public async Task<IActionResult> DeleteOrUpdate()
        //{
        //    return this.View();
        //}

        [HttpPost("Teacher/Bookmark")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Bookmark(MarkBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var marks = new Dictionary<string, IEnumerable<ISubject>>();
            var bookmark = await bookmarkService.GetBookmarkByUsernameAsync(model.StudentUsername);

            studentUsername = model.StudentUsername;
            ViewData["StudentUsername"] = studentUsername;

            var philosophy = bookmark.PhilosophyMarks.ToList();

            var english = bookmark.EnglishMarks.ToList();

            var mathematics = bookmark.MathematicsMarks.ToList();

            marks["Philosophy"] = philosophy;
            marks["Mathematics"] = mathematics;
            marks["English"] = english;

            return this.View(marks);
        }

        [HttpGet("/Teacher/Delete/{MarkId}/{BookmarkId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(string MarkId, string BookmarkId)
        {
            await markService.DeleteMarkAsync(MarkId, BookmarkId);
            return this.Redirect("/Teacher/Marks");
        }
    }
}