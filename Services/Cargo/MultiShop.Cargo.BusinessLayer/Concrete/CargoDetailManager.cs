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
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoDetailDal _cargoDetailDal;

        public CargoDetailManager(ICargoDetailDal cargoDetailDal)
        {
            _cargoDetailDal = cargoDetailDal;
        }

        public void BAdd(CargoDetail entity)
        {
            _cargoDetailDal.Add(entity);
        }

        public void BDelete(int id)
        {
            _cargoDetailDal.Delete(id);
        }

        public CargoDetail BGetById(int id)
        {
            return _cargoDetailDal.GetById(id);
        }

        public List<CargoDetail> BGetListAll()
        {
            return _cargoDetailDal.GetListAll();
        }

        public void BUpdate(CargoDetail entity)
        {
            _cargoDetailDal.Update(entity);
        }
    }
}
