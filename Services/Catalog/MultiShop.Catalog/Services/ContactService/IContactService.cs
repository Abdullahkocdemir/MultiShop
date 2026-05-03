using MultiShop.Catalog.DTOs.ContactDTO;

namespace MultiShop.Catalog.Services.ContactService
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
