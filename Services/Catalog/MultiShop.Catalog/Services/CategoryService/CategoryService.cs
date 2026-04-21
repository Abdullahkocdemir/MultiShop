using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.CategoryDTO;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultCategoryDTO>>(values);
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var value = _mapper.Map<Category>(createCategoryDTO);
            await _categoryCollection.InsertOneAsync(value);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            var values = _mapper.Map<Category>(updateCategoryDTO);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategoryDTO.CategoryId, values);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryId == id);
        }

        public async Task<GetByIdCategoryDTO> GetByIdCategoryAsync(string id)
        {
            var value = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryDTO>(value);
        }

        public async Task StatusChangeCategoryTrueAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq(x => x.CategoryId, id);
            var update = Builders<Category>.Update.Set(x => x.IsActive, true);
            await _categoryCollection.UpdateOneAsync(filter, update);
        }

        public async Task StatusChangeCategoryFalseAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq(x => x.CategoryId, id);
            var update = Builders<Category>.Update.Set(x => x.IsActive, false);
            await _categoryCollection.UpdateOneAsync(filter, update);
        }
    }
}