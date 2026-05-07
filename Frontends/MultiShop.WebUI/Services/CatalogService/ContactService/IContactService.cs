using MultiShop.DTOLayer.CatalogDTOs.ContactDTO;

namespace MultiShop.WebUI.Services.CatalogService.ContactService
{
    public interface IContactService
    {
        Task<List<ResultContactDTO>> GetAllContactAsync();
        Task CreateContactAsync(CreateContactDTO createContactDTO);
        Task UpdateContactAsync(UpdateContactDTO updateContactDTO);
        Task DeleteContactAsync(string id);
        Task<GetByIdContactDTO> GetByIdContactAsync(string id);
    }
}
