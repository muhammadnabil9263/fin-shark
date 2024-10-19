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

    // POST api/comments
    //[HttpPost("stockId")]
    //public async Task<IActionResult> Post( CreateCommentDTO commentDto, int stockId)
    //{

    //    if (!await _stockRepository.StockExists(stockId)) {
    //        return BadRequest("Stock not exist");
    //    }
    //    var comment = CommentMapper.ToCommentModelFromCreateDTO(commentDto , stockId);

    //    await _commentRepository.CreateAsync(comment);

    //    return Ok(await _commentRepository.CreateAsync(comment));

    //}

    [HttpPost]
    public async Task<IActionResult> Post(CreateCommentDTO commentDTO)
    {

        if (!await _stockRepository.StockExists(commentDTO.StockId))
        {
            return BadRequest("stock not found ");
        }

        var comment = CommentMapper.ToCommentModelFromCreateDTO(commentDTO);

        await _commentRepository.CreateAsync(comment);

        return Ok(new { message = "Stock created successfully.", co = comment });
    }



    // PUT  api/comments/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/comments/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
