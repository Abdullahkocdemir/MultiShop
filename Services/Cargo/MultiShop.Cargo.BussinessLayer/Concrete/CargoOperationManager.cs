using MultiShop.Cargo.BussinessLayer.Absract;
using MultiShop.Cargo.DataAccessLayer.Absract;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.BussinessLayer.Concrete
{
    public class CargoOperationManager : ICargoOperationService
    {
        private readonly ICargoOperationDal _cargoOperationDal;
        public CargoOperationManager(ICargoOperationDal cargoOperationDal)
        {
            _cargoOperationDal = cargoOperationDal;
        }
        public async Task BAddAsync(CargoOperation entity)
        {
            await _cargoOperationDal.AddAsync(entity);
        }

        public async Task BDeleteAsync(int id)
        {
            await _cargoOperationDal.DeleteAsync(id);
        }

        public async Task<List<CargoOperation>> BGetAllAsync()
        {
            return await _cargoOperationDal.GetAllAsync();
        }

        public async Task<CargoOperation> BGetByIdAsync(int id)
        {
            return await _cargoOperationDal.GetByIdAsync(id);
        }

        public async Task BUpdateAsync(CargoOperation entity)
        {
            await _cargoOperationDal.UpdateAsync(entity);
        }
    }
}
