using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopUI.Models;

namespace WebShopUI.Services
{
    public class DbService : IDatasource<ProductViewModel>
    {
        private readonly ProductDbContext _context;

        public DbService(ProductDbContext context)
        {
            _context = context;
        }

        public async void Remove(ProductViewModel product)
        {
            _context.ProductViewModel.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveAsync(ProductViewModel product)
        {
            _context.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductViewModel> SingleAsync(int? id)
        {
            return await _context.ProductViewModel
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<ProductViewModel>> ToListAsync()
        {
            return await _context.ProductViewModel.ToListAsync();
        }

        public async Task<int> UpdateAsync(ProductViewModel product)
        {
            _context.Update(product);
            return await _context.SaveChangesAsync();
        }
    }
}
