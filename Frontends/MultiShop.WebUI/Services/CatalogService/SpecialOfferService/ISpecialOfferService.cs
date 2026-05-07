using MultiShop.DTOLayer.CatalogDTOs.SpecialOfferDTO;

namespace MultiShop.WebUI.Services.CatalogService.SpecialOfferService
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
