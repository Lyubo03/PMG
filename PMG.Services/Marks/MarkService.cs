namespace PMG.Services.Marks
{
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using PMG.Domain;
    using PMG.Domain.SchoolSubjects;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MarkService : IMarkService
    {
        private readonly PMGDbContext context;
        public MarkService(PMGDbContext context)
        {
            this.context = context;
        }

        public async Task CreateMarkEnglishAsync(decimal mark, PMGUser user)
        {
            English english = new English
            {
                Mark = mark,
                Day = DateTime.UtcNow
            };
            user.Bookmark.EnglishMarks.Add(english);
            await context.SaveChangesAsync();
        }

        public async Task CreateMarkMathematicsAsync(decimal mark, PMGUser user)
        {
            Mathematics math = new Mathematics
            {
                Mark = mark,
                Day = DateTime.UtcNow
            };
            user.Bookmark.MathematicsMarks.Add(math);
            await context.SaveChangesAsync();
        }

        public async Task CreateMarkPhilosophyAsync(decimal mark, PMGUser user)
        {
            Philosophy philosophy = new Philosophy
            {
                Mark = mark,
                Day = DateTime.UtcNow
            };
            user.Bookmark.PhilosophyMarks.Add(philosophy);
            await context.SaveChangesAsync();
        }
        public async Task DeleteMarkAsync(string MarkId, string bookmarkId)
        {
            var bookmark = context.Bookmarks
                .Include(x => x.EnglishMarks)
                .Include(x => x.PhilosophyMarks)
                .Include(x => x.MathematicsMarks)
                .SingleOrDefault(x => x.Id == bookmarkId);

            var english = bookmark.EnglishMarks.SingleOrDefault(x => x.Id == MarkId);
            var philosophy = bookmark.PhilosophyMarks.SingleOrDefault(x => x.Id == MarkId);
            var mathematics = bookmark.MathematicsMarks.SingleOrDefault(x => x.Id == MarkId);

            if (english != null)
            {
                context.English.Remove(english);
            }
            else if (philosophy != null)
            {
                context.Philosophy.Remove(philosophy);
            }
            else if (mathematics != null)
            {
                context.Mathematics.Remove(mathematics);
            }

            await context.SaveChangesAsync();
        }
    }
}