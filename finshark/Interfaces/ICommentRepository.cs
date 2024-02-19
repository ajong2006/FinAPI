using finshark.Helpers;
using finshark.Models;

namespace finshark.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment> DeleteAsync(int id);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);

    }
}
