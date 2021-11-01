using FashionShop.Models;
using FashionShop.Data;
using FashionShop.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<Color> _colors;
        private IRepository<Size> _sizes;
        private IRepository<Style> _styles;
        private IRepository<Product> _products;
        private IRepository<Category> _categories;
        private IRepository<ProductSize> _productSizes;
        private IRepository<Gender> _genders;
        private IRepository<Material> _materials;
        private IRepository<MaterialCategory> _materialCategories;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Color> Colors => _colors ??= new Repository<Color>(_context);

        public IRepository<Size> Sizes => _sizes ??= new Repository<Size>(_context);

        public IRepository<Product> Products => _products ??= new Repository<Product>(_context);

        public IRepository<Category> Categories => _categories ??= new Repository<Category>(_context);

        public IRepository<Style> Styles => _styles ??= new Repository<Style>(_context);

        public IRepository<ProductSize> ProductSizes => _productSizes ??= new Repository<ProductSize>(_context);


        public IRepository<Gender> Genders => _genders ??= new Repository<Gender>(_context);

        public IRepository<Material> Materials => _materials ??= new Repository<Material>(_context);

        public IRepository<MaterialCategory> MaterialCategories => _materialCategories ??= new Repository<MaterialCategory>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}