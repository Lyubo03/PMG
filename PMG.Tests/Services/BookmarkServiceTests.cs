namespace PMG.Tests.Services
{
    using Xunit;
    using Factory;
    using Data;
    using Domain;
    using PMG.Services.Bookmark;
    using System.Linq;
    using System.Collections.Generic;
    using PMG.Domain.SchoolSubjects;
    using System;
    using System.Threading.Tasks;

    public class BookmarkServiceTests
    {
        private static Bookmark Bookmark = new Bookmark();
        private static string BookmarkId = Bookmark.Id;

        private List<PMGUser> GetTestData = new List<PMGUser>()
        {  new PMGUser
                {
                  UserName = "Teacher",
                  Email = "TestUser1@mail.com",
                },
                new PMGUser
                {
                    UserName = "Student",
                    Email = "TestUser2@mail.com",
                    Bookmark = new Bookmark()
                }
        };
        private async Task SeedTestData(PMGDbContext context)
        {
            context.Bookmarks.Add(Bookmark);
            context.Users.AddRange(GetTestData);

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task TestAddBookmark_WithTestData_ShouldReturnBookmark()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();

            var bookmarkService = new BookmarkService(context);
            await bookmarkService.AddBookmarkAsync(Bookmark);

            var expectedData = Bookmark.Id;
            var actualData = context.Bookmarks.SingleOrDefault(x => x.Id == Bookmark.Id).Id;

            Assert.Equal(expectedData, actualData);
        }
        [Fact]
        public async Task GetBookmarkByUsername_WithTestData_ShouldReturn()
        {
            var username = "Student";
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var bookmarkService = new BookmarkService(context);
            await SeedTestData(context);

            var bookmark = await bookmarkService.GetBookmarkByUsernameAsync(username);

            var actualData = bookmark.Id;
            var expectedData = context.Users.SingleOrDefault(x => x.UserName == username).BookmarkId;

            Assert.Equal(expectedData, actualData);
        }
    }
}