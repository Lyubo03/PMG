namespace PMG.Tests.Services
{
    using PMG.Data;
    using PMG.Domain;
    using PMG.Domain.SchoolSubjects;
    using PMG.Services.Marks;
    using PMG.Tests.Factory;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class MarksServiceTests
    {
        private static Bookmark Bookmark = new Bookmark();
        public PMGUser User = new PMGUser { UserName = "Student", Bookmark = new Bookmark(), Email = "student@abv.bg" };

        private async Task SeedBookmarkWithMarks(PMGDbContext context)
        {
            var mathMark = new Mathematics()
            {
                Mark = 3,
                bookmark = Bookmark,
                Day = DateTime.UtcNow
            };

            var englishMark = new English()
            {
                Mark = 4,
                bookmark = Bookmark,
                Day = DateTime.UtcNow
            };

            var philosophyMark = new Philosophy()
            {
                Mark = 5,
                bookmark = Bookmark,
                Day = DateTime.UtcNow
            };

            context.Mathematics.Add(mathMark);
            context.Add(englishMark);
            context.Philosophy.Add(philosophyMark);

            context.SaveChanges();
        }

        //[Fact]
        //private void TestDeleteMathematicsMark_WithTestData_ShouldReturnZeroCount()
        //{
        //    decimal mark = 3;
        //    string subject = "Mathematics";
        //    var context = PMGDbContextInMemoryFactory.CreateDbContext();
        //    var markService = new MarkService(context);
        //    SeedBookmarkWithMarks(context);

        //    markService.DeleteMark(mark, subject).GetAwaiter().GetResult();
            
        //    var mathMarks = context.Mathematics.ToList();
        //    Assert.True(mathMarks.Count == 0, "DeleteMark() does not work properly!");
        //}
        //[Fact]
        //private void TestDeletePhilosophyMark_WithTestData_ShouldReturnZeroCount()
        //{
        //    decimal mark = 5;
        //    string subject = "Philosophy";
        //    var context = PMGDbContextInMemoryFactory.CreateDbContext();
        //    var markService = new MarkService(context);
        //    SeedBookmarkWithMarks(context);

        //    markService.DeleteMark(mark, subject).GetAwaiter().GetResult();

        //    var philosophyMarks = context.Philosophy.ToList();
        //    Assert.True(philosophyMarks.Count == 0, "DeleteMark() does not work properly!");
        //}

        //[Fact]
        //private void TestDeleteEnglishMark_WithTestData_ShouldReturnZeroCount()
        //{
        //    decimal mark = 4;
        //    string subject = "English";
        //    var context = PMGDbContextInMemoryFactory.CreateDbContext();
        //    var markService = new MarkService(context);
        //    SeedBookmarkWithMarks(context);

        //    markService.DeleteMark(mark, subject).GetAwaiter().GetResult();

        //    var englishMarks = context.English.ToList();
        //    Assert.True(englishMarks.Count == 0, "DeleteMark() does not work properly!");
        //}

        [Fact]
        private async Task TestCreateMarkEnglish_WithTestData_ShouldNotReturnZeroCount()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var markService = new MarkService(context);

            var mark = 5.50m;

            await markService.CreateMarkEnglishAsync(mark, User);
            var actualResult = User.Bookmark.EnglishMarks.SingleOrDefault(x => x.Mark == mark).Mark;

            Assert.Equal(mark, actualResult);
        }

        [Fact]
        private async Task TestCreateMarkPhilosophy_WithTestData_ShouldNotReturnZeroCount()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var markService = new MarkService(context);

            var mark = 3.50m;

            await markService.CreateMarkPhilosophyAsync(mark, User);
            var actualResult = User.Bookmark.PhilosophyMarks.SingleOrDefault(x => x.Mark == mark).Mark;

            Assert.Equal(mark, actualResult);
        }

        [Fact]
        private async Task TestCreateMarkMathematics_WithTestData_ShouldNotReturnZeroCount()
        {
            var context = PMGDbContextInMemoryFactory.CreateDbContext();
            var markService = new MarkService(context);

            var mark = 4.50m;

            await markService.CreateMarkMathematicsAsync(mark, User);
            var actualResult = User.Bookmark.MathematicsMarks.SingleOrDefault(x => x.Mark == mark).Mark;

            Assert.Equal(mark, actualResult);
        }
    }
}