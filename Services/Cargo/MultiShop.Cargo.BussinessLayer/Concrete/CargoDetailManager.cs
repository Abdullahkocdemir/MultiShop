using MultiShop.Cargo.BussinessLayer.Absract;
using MultiShop.Cargo.DataAccessLayer.Absract;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.BussinessLayer.Concrete
{
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoDetailDal _cargoDetailDal;
        public CargoDetailManager(ICargoDetailDal cargoDetailDal)
        {
            _cargoDetailDal = cargoDetailDal;
        }
        public async Task BAddAsync(CargoDetail entity)
        {
            await _cargoDetailDal.AddAsync(entity);
        }

        public async Task BDeleteAsync(int id)
        {

            await _cargoDetailDal.DeleteAsync(id);
        }

        public async Task<List<CargoDetail>> BGetAllAsync()
        {

            return await _cargoDetailDal.GetAllAsync();
        }

        public async Task<CargoDetail> BGetByIdAsync(int id)
        {

            return await _cargoDetailDal.GetByIdAsync(id);
        }

        public async Task BUpdateAsync(CargoDetail entity)
        {
            await _cargoDetailDal.UpdateAsync(entity);
        }
    }
}
