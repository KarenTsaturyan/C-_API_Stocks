using System.ComponentModel.DataAnnotations;

namespace StockApi.Dtos.Comment
{
    public class CreateCommentRequestDto
    {
        //(DataAnnotations), in DTO, not in model(not to be global)
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 chars")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 chars")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280")]
        public string Content { get; set; } = string.Empty;
    }
}
