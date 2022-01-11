using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentService.Data
{
    public interface ICrud<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Insert(T entity);
        Task<T> Update(int id, T entity);
        Task Delete(int id);
    }
}
