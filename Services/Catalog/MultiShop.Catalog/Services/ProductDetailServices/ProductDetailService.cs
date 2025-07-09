using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDTO;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMongoCollection<ProductDetail> _productdetailCollection;
        private readonly IMapper _mapper;

        public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB istemcisi oluşturuluyor
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Veritabanı seçiliyor
            _productdetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO)
        {
            var value = _mapper.Map<ProductDetail>(createProductDetailDTO);
            await _productdetailCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _productdetailCollection.DeleteOneAsync(x => x.ProductDetailID == id);
        }

        public async Task<List<ResultProductDetailDTO>> GetAllProductDetailAsync()
        {
            var values = await _productdetailCollection.Find(x => true).ToListAsync();
            return (_mapper.Map<List<ResultProductDetailDTO>>(values));
        }

        public async Task<GetByIdProductDetailDTO> GetByIdProductDetailAsync(string id)
        {
            var value = await _productdetailCollection.Find(x => x.ProductDetailID == id).FirstOrDefaultAsync();
            return (_mapper.Map<GetByIdProductDetailDTO>(value));
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO)
        {
            var values = _mapper.Map<ProductDetail>(updateProductDetailDTO);
            await _productdetailCollection.FindOneAndReplaceAsync(x => x.ProductDetailID == updateProductDetailDTO.ProductDetailID, values);
        }
    }
}

