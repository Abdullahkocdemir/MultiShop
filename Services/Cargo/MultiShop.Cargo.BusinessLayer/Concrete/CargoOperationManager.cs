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
    public class CargoOperationManager : ICargoOperationService
    {
        private readonly ICargoOperationDal _cargoOperationDal;

        public CargoOperationManager(ICargoOperationDal cargoOperationDal)
        {
            _cargoOperationDal = cargoOperationDal;
        }

        public void BAdd(CargoOperation entity)
        {
            _cargoOperationDal.Add(entity);
        }

        public void BDelete(int id)
        {
            _cargoOperationDal.Delete(id);
        }

        public CargoOperation BGetById(int id)
        {
            return _cargoOperationDal.GetById(id);
        }

        public List<CargoOperation> BGetListAll()
        {
            return _cargoOperationDal.GetListAll();
        }

        public void BUpdate(CargoOperation entity)
        {
            _cargoOperationDal.Update(entity);
        }
    }
}
