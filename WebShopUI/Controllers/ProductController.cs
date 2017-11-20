using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShopUI.Models;
using Microsoft.Extensions.Caching.Memory;
using WebShopUI.Services;

namespace WebShopUI.Controllers
{
    public class ProductController : Controller
    {
        private static IDatasource<ProductViewModel> _ds;
        private readonly IMemoryCache _cache;
        private readonly ProductDbContext _context;
        static string _datasource = "database";

        void SetDatasource() {
            switch (_datasource)
            {
                case "memory":
                    _ds = new CacheService(_cache);                    
                    break;
                default:
                    //database by default
                    _ds = new DbService(_context);                    
                    break;
            }

        }

        //public ProductController(){
        //    SetDatasource("memory");
        //}

        public ProductController(IMemoryCache cache, ProductDbContext context)
        {
            _cache = cache;
            _context = context;
            SetDatasource();
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            ViewData["datasource"] = _datasource;
            return View(await _ds.ToListAsync());
        }

        // GET: Product/ChangeDataSource/
        public IActionResult ChangeDataSource()
        {
            _datasource = _datasource == "database"?"memory":"database";
            SetDatasource();
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _ds.SingleAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Name,Price")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                await _ds.SaveAsync(productViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _ds.SingleAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Name,Price")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ds.UpdateAsync(productViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productViewModel = await _ds.SingleAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productViewModel = await _ds.SingleAsync(id);
            _ds.Remove(productViewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
