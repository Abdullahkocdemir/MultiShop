using MultiShop.Cargo.BussinessLayer.Absract;
using MultiShop.Cargo.DataAccessLayer.Absract;
using MultiShop.Cargo.EntityLayer.Entities;

namespace MultiShop.Cargo.BussinessLayer.Concrete
{
    public class CargoCompanyManager : ICargoCompanyService
    {
        private readonly ICargoCompanyDal _cargoCompanyDal;
        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            _cargoCompanyDal = cargoCompanyDal;
        }
        public async Task BAddAsync(CargoCompany entity)
        {
            await _cargoCompanyDal.AddAsync(entity);
        }

        public async Task BDeleteAsync(int id)
        {
            await _cargoCompanyDal.DeleteAsync(id);
        }

        public async Task<List<CargoCompany>> BGetAllAsync()
        {
            return await _cargoCompanyDal.GetAllAsync();
        }

        public async Task<CargoCompany> BGetByIdAsync(int id)
        {
            return await _cargoCompanyDal.GetByIdAsync(id);
        }

        public async Task BUpdateAsync(CargoCompany entity)
        {
            await _cargoCompanyDal.UpdateAsync(entity);
        }
    }
}
