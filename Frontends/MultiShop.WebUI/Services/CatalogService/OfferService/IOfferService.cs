using MultiShop.DTOLayer.CatalogDTOs.OfferDiscountDTO;

namespace MultiShop.WebUI.Services.CatalogService.OfferService
{
    public interface IOfferService
    {
        Task<List<ResultOfferDiscountDTO>> GetAllOfferDiscountAsync();
        Task CreateOfferDiscountAsync(CreateOfferDiscountDTO createOfferDiscountDTO);
        Task UpdateOfferDiscountAsync(UpdateOfferDiscountDTO updateOfferDiscountDTO);
        Task DeleteOfferDiscountAsync(string id);
        Task<GetByIdOfferDiscountDTO> GetByIdOfferDiscountAsync(string id);
    }
}
