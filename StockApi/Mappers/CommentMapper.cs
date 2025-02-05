using StockApi.Dtos.Comment;
using StockApi.Dtos.Stock;
using StockApi.Models;

namespace StockApi.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commnetModel)
        {
            return new CommentDto
            {
                Id = commnetModel.Id,
                Title = commnetModel.Title,
                Content = commnetModel.Content,
                CreatedOn = commnetModel.CreatedOn,
                StockId = commnetModel.StockId,
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentRequestDto commnetDto, int stockId)
        {
            return new Comment
            {
                
                Title = commnetDto.Title,
                Content = commnetDto.Content,
                StockId = stockId,
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commnetDto)
        {
            return new Comment
            {

                Title = commnetDto.Title,
                Content = commnetDto.Content,
            };
        }

    }
}
