using MultiShop.DTOLayer.CommentDTOs;

namespace MultiShop.WebUI.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultCommentDTO>> GetAllCommentAsync()
        {
            // API: [HttpGet]
            var response = await _httpClient.GetAsync("comments");
            return await response.Content.ReadFromJsonAsync<List<ResultCommentDTO>>();
        }

        public async Task<GetByIdCommentDTO> GetByIdCommentAsync(string id)
        {
            // API: [HttpGet("{id}")]
            var response = await _httpClient.GetAsync($"comments/{id}");
            return await response.Content.ReadFromJsonAsync<GetByIdCommentDTO>();
        }

        public async Task CreateCommentAsync(CreateCommentDTO createCommentDTO)
        {
            // API: [HttpPost]
            await _httpClient.PostAsJsonAsync("comments", createCommentDTO);
        }

        public async Task UpdateCommentAsync(UpdateCommentDTO updateCommentDTO)
        {
            // API: [HttpPut]
            await _httpClient.PutAsJsonAsync("comments", updateCommentDTO);
        }

        public async Task DeleteCommentAsync(string id)
        {
            // API: [HttpDelete("{id}")]
            await _httpClient.DeleteAsync($"comments/{id}");
        }

        public async Task<List<ResultCommentDTO>> GetCommentsByProductIdAsync(string productId)
        {
            // API: [HttpGet("GetCommentByProductId/{productId}")]
            var response = await _httpClient.GetAsync($"comments/GetCommentByProductId/{productId}");
            return await response.Content.ReadFromJsonAsync<List<ResultCommentDTO>>();
        }

        public async Task<int> GetCommentCountByProductIdAsync(string productId)
        {
            // API: [HttpGet("GetCommentCount")]
            var response = await _httpClient.GetAsync($"comments/GetCommentCount?productId={productId}");
            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}