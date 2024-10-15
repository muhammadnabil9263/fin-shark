using api.Interfaces;
using api.Mappers;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
   private ICommentRepository _commentRepository;
    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }


    // GET: api/comments
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();

        var commentDTOs = comments.Select(comment => CommentMapper.ToDTO(comment)).ToList();
        return Ok(commentDTOs);
    }

    // GET api/comments/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/comments
    [HttpPost]
    public void Post([FromBody] string value)
    {
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
