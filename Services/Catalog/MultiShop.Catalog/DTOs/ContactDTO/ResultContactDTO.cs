namespace MultiShop.Catalog.DTOs.ContactDTO
{
    public class ResultContactDTO
    {
        public string ContactId { get; set; } = string.Empty;
        public string NameSurname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; } = false;
        public DateTimeOffset SendDate { get; set; }
    }
}
