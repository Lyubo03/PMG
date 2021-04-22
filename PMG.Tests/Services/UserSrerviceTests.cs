namespace PMG.Tests.Services
{
    using PMG.Data;
    using PMG.Domain;
    using PMG.Services.User;
    using PMG.Tests.Factory;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    public class UserSrerviceTests
    {
        private List<PMGUser> GetTestData = new List<PMGUser>()
        {       new PMGUser
                {
                  UserName = "Teacher",
                  Role = "Teacher",
                  Email = "TestUser1@mail.com",
                },
            
                new PMGUser
                {
                    UserName = "Student",
                    Email = "Student@mail.com",
                    Role = "Student",
                    Bookmark = new Bookmark()
                },
                new PMGUser
                {
                    UserName = "Ivan",
                    Email = "TestUser2@mail.com",
                    Role = "Student",
                    Bookmark = new Bookmark()
                },

                new PMGUser
                {
                    UserName = "Grigor",
                    Email = "TestUser3@mail.com",
                    Role = "Student",
                    Bookmark = new Bookmark()
                }
        };
        private async Task SeedTestData(PMGDbContext context)
        {
            context.Users.AddRange(GetTestData);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task TestGetAllUsers_WithTestData_ShouldReturnEverySingleUser()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();

            await SeedTestData(context);

            var userService = new UserService(context);

            var expectedData = GetTestData;
            var actualData = (await userService.GetAllUsersAsync()).ToList();

            Assert.Equal(expectedData.Count, actualData.Count());
            Assert.Equal<PMGUser>(expectedData, actualData);

            foreach (var actualUser in actualData)
            {
                Assert.True(expectedData.Any(user =>
                actualUser.UserName == user.UserName &&
                actualUser.Email == user.Email),
                "User Service GetAllUSers() doens't work properly");
            }
        }
        [Fact]
        public async Task TestGetAllUsers_WithoudAnyData_ShouldReturnEmpty()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();

            var userService = new UserService(context);

            var actualData = (await userService.GetAllUsersAsync()).ToList();

            Assert.True(actualData.Count() == 0,
                "UserService GetAllUsers() method does not work properly");
        }
        [Fact]
        public async Task TestGetUserWithBookmarkByUsrename_WithTestData_ShouldReturnUserWithBookmark()
        {
            var username = "Student";
            var context = PMGDbContextInMemoryFactory.CreateDbContext();

            await SeedTestData(context);

            var userService = new UserService(context);

            var user = await userService.GetUserWithBookmarkByUsrenameAsync(username);

            Assert.Equal(user.UserName, username);
            Assert.False(user.BookmarkId == null);
        }

        [Fact]
        public async Task TestGetUserWithBookmarkByUsrename_WithTestData_ShouldReturnNull()
        {
            var username = "StudentIvan";
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            await SeedTestData(context);

            var userService = new UserService(context);

            var user = await userService.GetUserWithBookmarkByUsrenameAsync(username);

            Assert.True(user == null, "User Service GetAllUSersWithBookmarkByUsrename() doens't work properly");
        }

        [Fact]
        public async Task TestGetAllStudents_WithTestData_ShouldReturnNull()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            await SeedTestData(context);
            bool isStudent = true;

            var userService = new UserService(context);
            var students = await userService.GetAllStudentsAsync();

            foreach (var student in students)
            {
                if (student.Role != "Student")
                {
                    isStudent = false;
                    return;
                }
            }

            Assert.True(isStudent, "User Service GetAllStudents() doens't work properly");
        }
    }
}