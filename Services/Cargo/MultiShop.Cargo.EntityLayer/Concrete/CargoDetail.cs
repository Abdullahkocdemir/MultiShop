using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoDetail
    {
        [Key]
        public int Id { get; set; }
        public string SenderCustomer { get; set; } = string.Empty;
        public string ReceiverCustomer { get; set; } = string.Empty;
        public int Barcode { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
    }
}
