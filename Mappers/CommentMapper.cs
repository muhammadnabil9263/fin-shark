using api.DTOs.Comment;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers;

public class CommentMapper
{

    public static CommentDTO ToDTO(Models.Comment comment)
    {
        return new CommentDTO
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            Title = comment.Title,
            StockId = comment.StockId,
        };
    }


    public static Models.Comment ToCommentModelFromCreateDTO(CreateCommentDTO  commentDTO )
    {
        return new Models.Comment
        {
        Content = commentDTO.Content,
        Title = commentDTO.Title,
        StockId= commentDTO.StockId,
        };
    }
}
