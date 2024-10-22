using api.DTOs.Comment;
using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();

    // Create a new stock entry
    Task<Comment> CreateAsync(Comment comment);

    // Get a specific stock by ID
    Task<Stock?> GetByIdAsync(int id);

    // Update an existing stock record
    Task<bool> UpdateAsync(int id, Comment comment);

    // Delete a stock record
    Task<bool> DeleteAsync(int id);

}
