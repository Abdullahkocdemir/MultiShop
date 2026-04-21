using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.FeatureSliderDTO;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureSliderService
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
        private readonly IMapper _mapper;

        public FeatureSliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(_databaseSettings.FeatureSliderCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultFeatureSliderDTO>> GetAllFeatureSliderAsync()
        {
            var values = await _featureSliderCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultFeatureSliderDTO>>(values);
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDTO createFeatureSliderDTO)
        {
            var value = _mapper.Map<FeatureSlider>(createFeatureSliderDTO);
            await _featureSliderCollection.InsertOneAsync(value);
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDTO updateFeatureSliderDTO)
        {
            var values = _mapper.Map<FeatureSlider>(updateFeatureSliderDTO);
            await _featureSliderCollection.FindOneAndReplaceAsync(x => x.FeatureSliderId == updateFeatureSliderDTO.FeatureSliderId, values);
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
            await _featureSliderCollection.DeleteOneAsync(x => x.FeatureSliderId == id);
        }

        public async Task<GetByIdFeatureSliderDTO> GetByIdFeatureSliderAsync(string id)
        {
            var value = await _featureSliderCollection.Find(x => x.FeatureSliderId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdFeatureSliderDTO>(value);
        }


        public async Task FeatureSliderChageStatusToTrue(string id)
        {
            var filter = Builders<FeatureSlider>.Filter.Eq(x => x.FeatureSliderId, id);
            var update = Builders<FeatureSlider>.Update.Set(x => x.Status, true);
            await _featureSliderCollection.UpdateOneAsync(filter, update);
        }

        public async Task FeatureSliderChageStatusToFalse(string id)
        {
            var filter = Builders<FeatureSlider>.Filter.Eq(x => x.FeatureSliderId, id);
            var update = Builders<FeatureSlider>.Update.Set(x => x.Status, false);
            await _featureSliderCollection.UpdateOneAsync(filter, update);
        }
    }
}