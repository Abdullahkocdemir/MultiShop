using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void BAdd(T entity);
        void BDelete(int id);
        void BUpdate(T entity);
        T BGetById(int id);
        List<T> BGetListAll();
    }
}
