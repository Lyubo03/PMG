namespace PMG.Services.User
{
    using Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<PMGUser> GetUserWithBookmarkByUsrenameAsync(string username);
        Task<IEnumerable<PMGUser>> GetAllUsersAsync();
        Task<IEnumerable<PMGUser>> GetAllStudentsAsync();
    }
}