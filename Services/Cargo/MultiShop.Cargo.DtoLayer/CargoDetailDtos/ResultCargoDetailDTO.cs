using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.CargoDetailDtos
{
    public class ResultCargoDetailDTO
    {
        public int Id { get; set; }
        public string SenderCustomer { get; set; } = string.Empty;
        public string ReceiverCustomer { get; set; } = string.Empty;
        public int Barcode { get; set; }
        public int CargoCompanyId { get; set; }
    }
}
