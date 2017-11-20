using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopUI.Models;

namespace WebShopUI.Services
{
    public class CacheService : IDatasource<ProductViewModel>
    {
        private static IMemoryCache _cache;
        private static int _index = 0;

        public CacheService(IMemoryCache cache) {
            _cache = cache;
        }

        public void Remove(ProductViewModel product)
        {
            var productList = GetAll();
            if (productList != null)
            {
                productList.RemoveAll(p => p.Id == product.Id);
            }
        }

        public Task<int> SaveAsync(ProductViewModel product)
        {
            try
            {
                var productList = GetAll();
                product.Id = ++_index;
                productList.Add(product);
                Save(productList);
                return Task.FromResult(1);
            }
            catch { return Task.FromResult(0); }
        }

        public Task<ProductViewModel> SingleAsync(int? id)
        {
            var product = GetById(id);
            return Task.FromResult(product);
        }

        public Task<List<ProductViewModel>> ToListAsync()
        {
            var productList = GetAll();
            return Task.FromResult(productList);
        }

        public Task<int> UpdateAsync(ProductViewModel product)
        {
            throw new NotImplementedException();
        }

        List<ProductViewModel> GetAll()
        {
            var productList = _cache.Get<List<ProductViewModel>>("Product");
            return productList ?? new List<ProductViewModel>();
        }

        void Save(IEnumerable<ProductViewModel> productList)
        {
            _cache.Set("Product", productList);
        }

        ProductViewModel GetById(int? id)
        {
            var productList = GetAll();
            return productList.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
