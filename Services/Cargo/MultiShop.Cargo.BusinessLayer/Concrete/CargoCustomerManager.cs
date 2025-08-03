using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstruct;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    // ICargoCustomerService implement edin (bu zaten IGenericService<CargoCustomer>'dan miras alıyor)
    public class CargoCustomerManager : ICargoCustomerService
    {
        private readonly ICargoCustomerDal _cargoCustomerDal;

        public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal)
        {
            _cargoCustomerDal = cargoCustomerDal;
        }

        public void BAdd(CargoCustomer entity)
        {
            _cargoCustomerDal.Add(entity);
        }

        public void BDelete(int id)
        {
            _cargoCustomerDal.Delete(id);
        }

        public CargoCustomer BGetById(int id)
        {
            return _cargoCustomerDal.GetById(id);
        }

        public List<CargoCustomer> BGetListAll()
        {
            return _cargoCustomerDal.GetListAll();
        }

        public void BUpdate(CargoCustomer entity)
        {
            _cargoCustomerDal.Update(entity);
        }
    }
}