using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.OfferDiscountDTO;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.OfferService
{
    public class OfferService : IOfferService
    {
        private readonly IMongoCollection<OfferDiscount> _offerDiscountCollection;
        private readonly IMapper _mapper;

        public OfferService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _offerDiscountCollection = database.GetCollection<OfferDiscount>(_databaseSettings.OfferDiscountCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultOfferDiscountDTO>> GetAllOfferDiscountAsync()
        {
            var values = await _offerDiscountCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultOfferDiscountDTO>>(values);
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDTO createOfferDiscountDTO)
        {
            var value = _mapper.Map<OfferDiscount>(createOfferDiscountDTO);
            await _offerDiscountCollection.InsertOneAsync(value);
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDTO updateOfferDiscountDTO)
        {
            var values = _mapper.Map<OfferDiscount>(updateOfferDiscountDTO);
            await _offerDiscountCollection.FindOneAndReplaceAsync(x => x.OfferDiscountId == updateOfferDiscountDTO.OfferDiscountId, values);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _offerDiscountCollection.DeleteOneAsync(x => x.OfferDiscountId == id);
        }

        public async Task<GetByIdOfferDiscountDTO> GetByIdOfferDiscountAsync(string id)
        {
            var value = await _offerDiscountCollection.Find(x => x.OfferDiscountId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdOfferDiscountDTO>(value);
        }
    }
}