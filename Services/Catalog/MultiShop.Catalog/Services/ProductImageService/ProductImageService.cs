using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.ProductImageDTO;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageService
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _productImageCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }

        public async Task<List<ResultProductImageDTO>> GetAllProductImageAsync()
        {
            var values = await _productImageCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductImageDTO>>(values);
        }

        public async Task CreateProductImageAsync(CreateProductImageDTO createProductImageDTO)
        {
            var value = _mapper.Map<ProductImage>(createProductImageDTO);
            await _productImageCollection.InsertOneAsync(value);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDTO updateProductImageDTO)
        {
            var values = _mapper.Map<ProductImage>(updateProductImageDTO);
            await _productImageCollection.FindOneAndReplaceAsync(x => x.ProductImageId == updateProductImageDTO.ProductImageId, values);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _productImageCollection.DeleteOneAsync(x => x.ProductImageId == id);
        }

        public async Task<GetByIdProductImageDTO> GetByIdProductImageAsync(string id)
        {
            var value = await _productImageCollection.Find(x => x.ProductImageId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDTO>(value);
        }

        public async Task<List<GetByIdProductImageDTO>> GetByProductIdProductImageAsync(string id)
        {
            var values = await _productImageCollection.Find(x => x.ProductId == id).ToListAsync();
            return _mapper.Map<List<GetByIdProductImageDTO>>(values);
        }
    }
}