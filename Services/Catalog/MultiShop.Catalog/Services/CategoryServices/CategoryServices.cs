using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDTO;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices 
    {
        private readonly IMongoCollection<Category> _categoryCollection; 
        private readonly IMapper _mapper; 

        public CategoryServices(IMapper mapper, IDatabaseSettings _databaseSettings) 
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB istemcisi oluşturuluyor
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Veritabanı seçiliyor
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); 
            _mapper = mapper; 
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO) // Yeni kategori oluşturma metodu
        {
            var value = _mapper.Map<Category>(createCategoryDTO); // DTO → Entity dönüşümü
            await _categoryCollection.InsertOneAsync(value); // MongoDB'ye yeni doküman ekleniyor
        }

        public async Task DeleteCategoryAsync(string id) // ID'ye göre kategori silme
        {
            await _categoryCollection.DeleteOneAsync(x => x.CategoryID == id); // Belirtilen ID'ye sahip dokümanı sil
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync() // Tüm kategorileri listeleme
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync(); // Koleksiyondaki tüm dokümanları getir
            return (_mapper.Map<List<ResultCategoryDTO>>(values)); // Entity listesini DTO listesine dönüştür
        }

        public async Task<GetByIdCategoryDTO> GetByIdCategoryAsync(string id) // ID'ye göre tek kategori getir
        {
            var value = await _categoryCollection.Find(x => x.CategoryID == id).FirstOrDefaultAsync(); // ID eşleşen ilk dokümanı getir
            return (_mapper.Map<GetByIdCategoryDTO>(value)); // Entity → DTO dönüşümü
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO) // Kategori güncelleme metodu
        {
            var values = _mapper.Map<Category>(updateCategoryDTO); // DTO → Entity dönüşümü
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryID == updateCategoryDTO.CategoryID, values); // Eşleşen dokümanı yenisiyle değiştir
        }
    }
}
