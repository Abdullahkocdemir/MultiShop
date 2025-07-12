using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands
{
    public class RemoveAddressCommand
    {
        //Command sınıfı bizim ekleme silme güncelleme gibi işlemlerde parametrelerimizi tutar
        public RemoveAddressCommand(int id)
        {
            Id = id;
        }
        //Constructor geciyoruz cünkü bunun üzerinden nesne oluşturup cagıracagız.
        public int Id { get; set; }
    }
}
