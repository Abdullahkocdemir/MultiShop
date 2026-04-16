namespace MultiShop.Cargo.BussinessLayer.Absract
{
    public interface IGenericService<T> where T : class
    {
        Task<List<T>> BGetAllAsync();
        Task<T> BGetByIdAsync(int id);
        Task BAddAsync(T entity);
        Task BUpdateAsync(T entity);
        Task BDeleteAsync(int id);
    }
}
