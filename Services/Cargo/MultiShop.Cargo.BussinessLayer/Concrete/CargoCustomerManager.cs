using MultiShop.Cargo.BussinessLayer.Absract;
using MultiShop.Cargo.DataAccessLayer.Absract;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.BussinessLayer.Concrete
{
    public class CargoCustomerManager : ICargoCustomerService
    {
        private readonly ICargoCustomerDal _cargoCustomerDal;
        public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal)
        {
            _cargoCustomerDal = cargoCustomerDal;
        }
        public async Task BAddAsync(CargoCustomer entity)
        {
            await _cargoCustomerDal.AddAsync(entity);
        }

        public async Task BDeleteAsync(int id)
        {
            await _cargoCustomerDal.DeleteAsync(id);
        }

        public async Task<List<CargoCustomer>> BGetAllAsync()
        {
            return await _cargoCustomerDal.GetAllAsync();
        }

        public async Task<CargoCustomer> BGetByIdAsync(int id)
        {
            return await _cargoCustomerDal.GetByIdAsync(id);
        }

        public async Task BUpdateAsync(CargoCustomer entity)
        {
            await _cargoCustomerDal.UpdateAsync(entity);
        }
    }
}
