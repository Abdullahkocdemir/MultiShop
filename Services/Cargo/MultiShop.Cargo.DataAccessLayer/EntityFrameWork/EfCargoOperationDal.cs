using MultiShop.Cargo.DataAccessLayer.Abstruct;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.EntityFrameWork
{
    public class EfCargoOperationDal:GenericRepository<CargoOperation>, ICargoOperationDal
    {
        private readonly CargoContext _context;
        public EfCargoOperationDal(CargoContext context) : base(context) // Base sınıfın constructor'ını çağırır
        {
            _context = context; // Özel operasyonlar için local field'a da atanır
        }

    }
}
