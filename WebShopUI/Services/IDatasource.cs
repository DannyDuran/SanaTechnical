using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopUI.Services
{
    public interface IDatasource<T> where T: class
    {
        Task<List<T>> ToListAsync();

        Task<int> SaveAsync(T obj);

        Task<int> UpdateAsync(T obj);

        Task<T> SingleAsync(int? id);

        void Remove(T obj);
    }
}
