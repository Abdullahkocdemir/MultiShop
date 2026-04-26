namespace MultiShop.DTOLayer.CommentDTOs
{
    public class ResultCommentDTO
    {
        public string UserCommentId { get; set; } = string.Empty;
        public string NameSurname { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CommnetDetail { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
