using MultiShop.Catalog.DTOs.OfferDiscountDTO;

namespace MultiShop.Catalog.Services.OfferService
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
