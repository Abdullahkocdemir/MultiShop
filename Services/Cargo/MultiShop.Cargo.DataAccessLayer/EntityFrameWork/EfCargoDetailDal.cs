using MultiShop.Cargo.DataAccessLayer.Absract;
using MultiShop.Cargo.DataAccessLayer.Context;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.DataAccessLayer.EntityFrameWork
{
    public class EfCargoDetailDal : GenericRepository<CargoDetail>, ICargoDetailDal
    {
        public EfCargoDetailDal(CargoContext context) : base(context)
        {
        }
    }
}
