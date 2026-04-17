namespace MultiShop.Cargo.DTOLayer.DTOs.CargoOperation
{
    public class CreateCargoOperationDTO
    {
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset OperationDate { get; set; }
    }
}
