using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.CargoOperationDtos
{
    public class GetByIdCargoOperationDTO
    {
        public int Id { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime OperationDate { get; set; }
    }
}
