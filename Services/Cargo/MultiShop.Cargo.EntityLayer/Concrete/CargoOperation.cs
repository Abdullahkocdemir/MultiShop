using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoOperation
    {
        [Key]
        public int Id { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime OperationDate { get; set; }
    }
}
