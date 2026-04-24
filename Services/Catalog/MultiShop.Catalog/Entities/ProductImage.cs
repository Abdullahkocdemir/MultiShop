using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class ProductImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductImageId { get; set; }=string.Empty;
        public string Image1 { get; set; }=string.Empty;
        public string Image2 { get; set; }= string.Empty;
        public string Image3 { get; set; } = string.Empty;
        public string Image4 { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        [BsonIgnore]
        public Product Product { get; set; }
    }
}
