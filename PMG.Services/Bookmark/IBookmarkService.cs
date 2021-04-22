namespace PMG.Services.Bookmark
{
    using Domain;
    using System.Threading.Tasks;

    public interface IBookmarkService
    {
        Task AddBookmarkAsync(Bookmark bookmark);
        Task<Bookmark> GetBookmarkByUsernameAsync(string username);
        Task DeleteMarkAsync(string BookmarId, decimal mark, string subject);
    }
}