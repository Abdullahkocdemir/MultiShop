using MultiShop.DTOLayer.CommentDTOs;

namespace MultiShop.WebUI.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<ResultCommentDTO>> GetAllCommentAsync();
        Task CreateCommentAsync(CreateCommentDTO createCommentDTO);
        Task UpdateCommentAsync(UpdateCommentDTO updateCommentDTO);
        Task DeleteCommentAsync(string id);
        Task<GetByIdCommentDTO> GetByIdCommentAsync(string id);

        Task<List<ResultCommentDTO>> GetCommentsByProductIdAsync(string productId);
        Task<int> GetCommentCountByProductIdAsync(string productId);
    }
}