using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstruct;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoCompanyManager : ICargoCompanyService
    {
        private readonly ICargoCompanyDal _cargoCompanyDal;

        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            _cargoCompanyDal = cargoCompanyDal;
        }

        public void BAdd(CargoCompany entity)
        {
            _cargoCompanyDal.Add(entity);
        }

        public void BDelete(int id)
        {
            _cargoCompanyDal.Delete(id);
        }

        public CargoCompany BGetById(int id)
        {
            return _cargoCompanyDal.GetById(id);
        }

        public List<CargoCompany> BGetListAll()
        {
            return _cargoCompanyDal.GetListAll();
        }

        public void BUpdate(CargoCompany entity)
        {
            _cargoCompanyDal.Update(entity);
        }
    }
}
