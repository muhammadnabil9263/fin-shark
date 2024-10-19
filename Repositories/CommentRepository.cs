using api.Data;
using api.DTOs.Comment;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace api.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;


    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Comment comment)
    {

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
        
    }

}
