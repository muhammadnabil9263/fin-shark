using api.Data;
using api.DTOs.Comment;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();

    }

    // Create a new comment entry
    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;

    }

    // Get a specific comment  by ID
    public Task<Stock?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(int id, Comment updatedComment)
    {
        var existingComment = await _context.Comments.FindAsync(id);

        if (existingComment == null)
        {
            return false; // Comment not found
        }

        existingComment.Title = updatedComment.Title;
        existingComment.Content = updatedComment.Content;
      

        _context.Comments.Update(existingComment);
        await _context.SaveChangesAsync();

        return true; // Update successful
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }


}

