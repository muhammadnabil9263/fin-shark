using api.Data;
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
    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
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

    public async Task<bool> DeleteAsync(int id)
    {
        // Find the comment by ID
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
        {
            // Return false if the comment was not found
            return false;
        }
        // Remove the comment from the context
        _context.Comments.Remove(comment);

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return true indicating the comment was successfully deleted
        return true;
    }


}

