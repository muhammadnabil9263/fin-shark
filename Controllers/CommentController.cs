using api.Data;
using api.DTOs.Comment;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private ICommentRepository _commentRepository;
    private IStockRepository _stockRepository;
    private ApplicationDbContext _context;

    public CommentController(ICommentRepository commentRepository,
        IStockRepository stockRepository, ApplicationDbContext context)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
        _context = context;
    }


    // GET: api/comments
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();

        var commentDTOs = comments.Select(comment => CommentMapper.ToDTO(comment)).ToList();
        return Ok(commentDTOs);
    }



    [HttpPost]
    public async Task<IActionResult> Post(CreateCommentDTO commentDTO)
    {

        if (!await _stockRepository.StockExistsAsync(commentDTO.StockId))
        {
            return BadRequest("stock not found ");
        }

        var comment = CommentMapper.ToCommentModelFromCreateDTO(commentDTO);

        comment = await _commentRepository.CreateAsync(comment);

        return Ok(new { message = "Stock created successfully.", co = comment });
    }



    // PUT  api/comments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCommentDTO commentDTO)
    {
        if (commentDTO == null)
        {
            return BadRequest(new { message = "Invalid comment data." });
        }

        // Map the DTO to the comment model
        var comment = CommentMapper.ToCommentModelFromUpdateDTO(commentDTO);

        // Call the repository to update the stock
        var result = await _commentRepository.UpdateAsync(id, comment);

        if (result)
        {
            return Ok(new { message = "Comment updated successfully.", stock = commentDTO });
        }

        // Stock not found or update failed
        return NotFound(new { message = $"Comment with ID {id} not found." });
    }

    // DELETE api/comments/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
