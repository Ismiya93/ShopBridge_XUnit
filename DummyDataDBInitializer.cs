using ShopBridge_InventoryManagement.Core;
using System;
using System.Collections.Generic;
using System.Text;
using ShopBridge_InventoryManagement.Models;

namespace ProductServices
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }
        public void Seed(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Product.AddRange(
              new Products() {Name="Television", Description="LG",Price=25000 },
              new Products() { Name = "Mobile", Description = "Samsung", Price = 1000 },
              new Products() { Name = "Oven", Description = "Panasonic", Price = 10000 }
            );
            context.SaveChanges();
        }
    }
}
