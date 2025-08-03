using MultiShop.Cargo.DataAccessLayer.Abstruct;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.EntityFrameWork
{
    // Repository Pattern implementasyonu - CargoCompany için özel veri erişim sınıfı
    // ICargoCompanyDal interface'ini implement eder ve GenericRepository'den miras alır
    public class EfCargoCompanyDal : GenericRepository<CargoCompany>, ICargoCompanyDal
    {
        // Dependency Injection için DbContext referansı
        // Sadece bu sınıfa özel operasyonlar için kullanılır
        private readonly CargoContext _context;

        // Constructor - Dependency Injection container tarafından CargoContext inject edilir
        // base(context) -> Parent sınıfın (GenericRepository) constructor'ına context'i gönderir
        public EfCargoCompanyDal(CargoContext context) : base(context) //: base(context) // Parent sınıfın constructor'ını çağırır
        {
            _context = context; // Özel operasyonlar için local field'a da atanır
        }

        // Buraya CargoCompany'ye özel metodlar eklenebilir
        // Örnek: GetCompaniesByCity, GetActiveCompanies vs.
    }
}