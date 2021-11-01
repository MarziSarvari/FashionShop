using FashionShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Color> Colors { get; }
        IRepository<Size> Sizes { get; }
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        IRepository<Style> Styles { get; }
        IRepository<ProductSize> ProductSizes { get; }
        IRepository<Gender> Genders { get; }
        IRepository<Material> Materials { get; }
        IRepository<MaterialCategory> MaterialCategories { get; }
        Task Save();
    }
}