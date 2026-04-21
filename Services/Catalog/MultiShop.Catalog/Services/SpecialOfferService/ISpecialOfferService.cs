using MultiShop.Catalog.DTOs.SpecialOfferDTO;

namespace MultiShop.Catalog.Services.SpecialOfferService
{
    public interface ISpecialOfferService
    {
        Task<List<ResultSpecialOfferDTO>> GetAllSpecialOfferAsync();
        Task CreateSpecialOfferAsync(CreateSpecialOfferDTO createSpecialOfferDTO);
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDTO updateSpecialOfferDTO);
        Task DeleteSpecialOfferAsync(string id);
        Task<GetByIdSpecialOfferDTO> GetByIdSpecialOfferAsync(string id);
    }
}
