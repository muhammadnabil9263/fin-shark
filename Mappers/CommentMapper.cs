using api.DTOs.Comment;
using api.Models;

namespace api.Mappers;

public class CommentMapper
{

    public static CommentDTO ToDTO(Comment comment)
    {
        return new CommentDTO
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            Title = comment.Title,
            StockId = comment.StockId,        };
    }

}
