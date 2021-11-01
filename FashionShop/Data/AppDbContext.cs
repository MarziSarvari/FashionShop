﻿using FashionShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<Style> Styles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductSize> ProductSizes { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<MaterialCategory> MaterialCategories { get; set; }


    }
}
