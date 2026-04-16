using MultiShop.Cargo.DataAccessLayer.Absract;
using MultiShop.Cargo.DataAccessLayer.Context;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.DataAccessLayer.EntityFrameWork
{
    public class EfCargoOperationDal : GenericRepository<CargoOperation>, ICargoOperationDal
    {
        public EfCargoOperationDal(CargoContext context) : base(context)
        {
        }
    }
}
