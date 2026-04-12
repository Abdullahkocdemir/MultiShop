using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class ProductDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductDetailId { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductInfo { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        [BsonIgnore]
        public Product Product { get; set; }

    }
}
