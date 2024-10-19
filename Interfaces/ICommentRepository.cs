using api.DTOs.Comment;
using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{
    public Task<List<Comment>> GetAllAsync();
    Task CreateAsync(Comment comment);

}
