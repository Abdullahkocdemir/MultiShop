using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDTO;
using MultiShop.Catalog.Dtos.ProductImageDTO;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _productimageCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB istemcisi oluşturuluyor
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Veritabanı seçiliyor
            _productimageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }
        public async Task CreateProductImageAsync(CreateProductImageDTO createProductImageDTO)
        {
            var value = _mapper.Map<ProductImage>(createProductImageDTO);
            await _productimageCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _productimageCollection.DeleteOneAsync(x => x.ProductImageID == id);
        }

        public async Task<List<ResultProductImageDTO>> GetAllProductImageAsync()
        {
            var values = await _productimageCollection.Find(x => true).ToListAsync();
            return (_mapper.Map<List<ResultProductImageDTO>>(values));
        }

        public async Task<GetByIdProductImageDTO> GetByIdProductImageAsync(string id)
        {
            var value = await _productimageCollection.Find(x => x.ProductImageID == id).FirstOrDefaultAsync();
            return (_mapper.Map<GetByIdProductImageDTO>(value));
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDTO updateProductImageDTO)
        {
            var values = _mapper.Map<ProductImage>(updateProductImageDTO);
            await _productimageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == updateProductImageDTO.ProductImageID, values);
        }
    }
}
