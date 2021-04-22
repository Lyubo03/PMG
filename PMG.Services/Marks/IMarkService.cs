namespace PMG.Services.Marks
{
    using PMG.Domain;
    using System.Threading.Tasks;

    public interface IMarkService
    {
        Task CreateMarkMathematicsAsync(decimal mark, PMGUser user);
        Task CreateMarkPhilosophyAsync(decimal mark, PMGUser user);
        Task CreateMarkEnglishAsync(decimal mark, PMGUser user);
        Task DeleteMarkAsync(string MarkId, string BookmarkId);
    }
}