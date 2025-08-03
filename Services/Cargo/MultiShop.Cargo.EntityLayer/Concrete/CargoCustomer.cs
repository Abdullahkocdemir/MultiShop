using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoCustomer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

    }
}
