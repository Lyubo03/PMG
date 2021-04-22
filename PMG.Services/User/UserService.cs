namespace PMG.Services.User
{
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using PMG.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly PMGDbContext context;

        public UserService(PMGDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PMGUser>> GetAllUsersAsync()
        {
            var users = await context.Users.ToListAsync();
            return users;
        }
        public async Task<IEnumerable<PMGUser>> GetAllStudentsAsync()
        {
            var students = await context.Users
                .Where(x => x.Role == "Student").ToListAsync();

            return students;
        }

        public async Task<PMGUser> GetUserWithBookmarkByUsrenameAsync(string username)
        {
            var user = await context.Users
                .Include(x => x.Bookmark)
                .Include(x => x.Bookmark.EnglishMarks)
                .Include(x => x.Bookmark.EnglishMarks)
                .Include(x => x.Bookmark.MathematicsMarks)
                .SingleOrDefaultAsync(x => x.UserName == username);
            return user;
        }
    }
}