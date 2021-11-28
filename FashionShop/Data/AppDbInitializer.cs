using FashionShop.Models;
using FashionShop.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashionShop.Data
{
    public class AppDbInitializer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<IUnitOfWork>();
                var color = await context.Colors.GetAll();
                if (color.Count == 0)
                {
                    var colors = new List<Color>(){new Color()
                    {
                        Name = "Black",
                        Active = true
                    },
                    new Color()
                    {
                        Name = "Blue",
                        Active = true
                    }, new Color()
                    {
                        Name = "Yellow",
                        Active = true
                    }, new Color()
                    {
                        Name = "Green",
                        Active = true
                    }, new Color()
                    {
                        Name = "Brown",
                        Active = true
                    } };
                    await context.Colors.InsertRange(colors);
                    await context.Save();
                }

                var size = await context.Sizes.GetAll();
                if (size.Count == 0)
                {
                    var sizes = new List<Size>()
                    {
                    new Size()
                    {
                        Name = "Small",
                        Active = true
                    },
                    new Size()
                    {
                        Name = "Medium",
                        Active = true
                    }, new Size()
                    {
                        Name = "Large",
                        Active = true
                    }, new Size()
                    {
                        Name = "X-Large",
                        Active = true
                    }, new Size()
                    {
                        Name = "XX-Large",
                        Active = true
                    } };
                    await context.Sizes.InsertRange(sizes);
                    await context.Save();
                }

                var material = await context.Materials.GetAll();
                if (material.Count == 0)
                {
                    var materials = new List<Material>()
                    {
                    new Material()
                    {
                        Name = "Yarn",
                        Active = true
                    },
                    new Material()
                    {
                        Name = "Linen",
                        Active = true
                    }, new Material()
                    {
                        Name = "Leather",
                        Active = true
                    }, new Material()
                    {
                        Name = "Crepe",
                        Active = true
                    }, new Material()
                    {
                        Name = "Wool",
                        Active = true
                    } };
                    await context.Materials.InsertRange(materials);
                    await context.Save();
                }

                var category = await context.Categories.GetAll();
                if (category.Count == 0)
                {
                    var categories = new List<Category>()
                    {
                    new Category()
                    {
                        Name = "Pants",
                        Active = true
                    },
                    new Category()
                    {
                        Name = "Shirt",
                        Active = true
                    }, new Category()
                    {
                        Name = "Skirt",
                        Active = true
                    }, new Category()
                    {
                        Name = "Underwear",
                        Active = true
                    }, new Category()
                    {
                        Name = "Shorts",
                        Active = true
                    } };
                    await context.Categories.InsertRange(categories);
                    await context.Save();
                }

                var materialCategory = await context.MaterialCategories.GetAll();
                if (materialCategory.Count == 0)
                {
                    var materialCategories = new List<MaterialCategory>()
                    {
                    new MaterialCategory()
                    {
                       MaterialId = 1,
                       CategoryId=1
                    },
                    new MaterialCategory()
                    {
                        MaterialId = 5,
                        CategoryId = 2
                    }, new MaterialCategory()
                    {
                        MaterialId = 1,
                        CategoryId = 4
                    }, new MaterialCategory()
                    {
                        MaterialId = 4,
                        CategoryId = 2
                    }, new MaterialCategory()
                    {
                        MaterialId = 1,
                        CategoryId = 5
                    } };
                    await context.MaterialCategories.InsertRange(materialCategories);
                    await context.Save();
                }
                var gender = await context.Genders.GetAll();
                if (gender.Count == 0)
                {
                    var genders = new List<Gender>()
                    {
                    new Gender()
                    {
                        Name = "Male"
                    },
                    new Gender()
                    {
                        Name = "Female"
                    } };
                    await context.Genders.InsertRange(genders);
                    await context.Save();
                }


                var style = await context.Styles.GetAll();
                if (style.Count == 0)
                {
                    await context.Styles.InsertRange(new List<Style>() {new Style()
                    {
                        Name = "Legs",
                        GenderId = 1,
                        MaterialCategoryId=1,
                        Active = true
                    },
                    new Style()
                    {
                        Name = "Star",
                        GenderId = 2,
                        MaterialCategoryId=2,
                        Active = true
                    }, new Style()
                    {
                        Name = "Mandana",
                        GenderId = 2,
                        MaterialCategoryId=3,
                        Active = true
                    }, new Style()
                    {
                        Name = "Parmis",
                        GenderId = 2,
                        MaterialCategoryId=2,
                        Active = true
                    } });
                    await context.Save();

                }



                var product = await context.Products.GetAll();
                if (product.Count == 0)
                {
                    await context.Products.InsertRange(new List<Product>() {new Product()
                    {
                        StyleId = 2,
                        Description = "Long Shirt",
                        Active =true,
                        ColorId =5
                    },
                   new Product()
                   {
                       StyleId = 3,
                       Description = "Extra Long Shirt",
                       Active =true,
                       ColorId =1
                   }, new Product()
                   {
                       StyleId = 4,                       
                       Description = "Short Shirt",
                       Active =true,
                       ColorId =2
                   }, new Product()
                   {
                       StyleId = 1,                       
                       Description = "Long Pants",
                       Active =true,
                       ColorId =3
                   } });
                    await context.Save();
                }

                var productsize = await context.ProductSizes.GetAll();
                if (productsize.Count == 0)
                {
                    IEnumerable<ProductSize> productSizes = new List<ProductSize>(){new ProductSize()
                    {
                        ProductId=1,
                        Price = 300000,
                        SizeId = 1
                    },
                   new ProductSize()
                   {
                        ProductId=1,
                        Price = 320000,
                        SizeId = 2
                   },
                        new ProductSize()
                   {
                        ProductId=1,
                        Price = 350000,
                        SizeId = 3
                   }, new ProductSize()
                   {
                       ProductId=1,
                       Price = 360000,
                       SizeId = 4
                   }, new ProductSize()
                   {
                       ProductId=1,
                       Price = 360000,
                       SizeId = 5
                   } ,
                    new ProductSize()
                    {
                        ProductId=2,
                        Price = 360000,
                        SizeId = 1
                    },
                   new ProductSize()
                   {
                        ProductId=2,
                        Price = 360000,
                        SizeId = 2
                   },
                        new ProductSize()
                   {
                        ProductId=2,
                        Price = 360000,
                        SizeId = 3
                   }, new ProductSize()
                   {
                       ProductId=2,
                       Price = 360000,
                        SizeId = 4
                   }, new ProductSize()
                   {
                       ProductId=2,
                       Price = 360000,
                        SizeId = 5
                   },
                    new ProductSize()
                    {
                        ProductId=3,
                        Price = 400000,
                        SizeId = 1
                    },
                   new ProductSize()
                   {
                        ProductId=3,
                        Price = 410000,
                        SizeId = 2
                   },
                        new ProductSize()
                   {
                        ProductId=3,
                        Price = 420000,
                        SizeId = 3
                   }, new ProductSize()
                   {
                       ProductId=3,
                       Price = 430000,
                        SizeId = 4
                   }, new ProductSize()
                   {
                       ProductId=3,
                       Price = 440000,
                        SizeId = 5
                   },new ProductSize()
                    {
                        ProductId=4,
                        Price=200000,
                        SizeId = 1
                    },
                   new ProductSize()
                   {
                        ProductId=4,
                        Price=200000,
                        SizeId = 2
                   },
                        new ProductSize()
                   {
                        ProductId=4,
                        Price=220000,
                        SizeId = 3
                   }, new ProductSize()
                   {
                       ProductId=4,
                       Price=220000,
                        SizeId = 4
                   }, new ProductSize()
                   {
                       ProductId=4,
                       Price=220000,
                        SizeId = 5
                   }};
                    await context.ProductSizes.InsertRange(productSizes);
                    await context.Save();
                }
            }
        }
    }
}
