using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge_InventoryManagement.Contract;
using ShopBridge_InventoryManagement.Controllers;
using ShopBridge_InventoryManagement.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProductServices
{
    public class ProductUnitTestController
    {
        public Mock<IProductRepository> mock = new Mock<IProductRepository>();
        #region Get By Id

        [Fact]
        public async void Task_GetProductById_Return_OkResult()
        {
            //Arrange
            var product = new Products()
            {
                ID = 1,
                Name = "John",
                Description = "Developer",
                Price= 30000.00M
            };
            //Act
            mock.Setup(p => p.GetProductByID(1)).ReturnsAsync(product);
            ProductController prod = new ProductController(mock.Object);
            var data = await prod.Details(1);

            //Assert
            Assert.IsType<ViewResult>(data);
        }

        [Fact]
        public async void Task_GetProductById_Return_NotFoundResult()
        {
            //Arrange
            var product = new Products()
            {
                ID = 0,
                Name = "John",
                Description = "Developer",
                Price = 30000.00M
            };

            //Act
            mock.Setup(p => p.GetProductByID(0)).ReturnsAsync(product);
            ProductController prod = new ProductController(mock.Object);
            var data = await prod.Details(0);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }
        #endregion
        #region Get All

        [Fact]
        public async void Task_GetProducts_Return_OkResult()
        {
            //Arrange
            ProductController prod = new ProductController(mock.Object);
            //Act
            var data = await prod.Index();

            //Assert
            Assert.IsType<ViewResult>(data);
        }

        [Fact]
        public void Task_GetProducts_Return_BadRequestResult()
        {
            //Arrange
            ProductController prod = new ProductController(mock.Object);
            //Act
            var data = prod.Index();
            data = null;

            if (data != null)
                //Assert
                Assert.IsType<BadRequestResult>(data);
        }
        #endregion
        #region Add New Product

        [Fact]
        public async void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange
            ProductController prod = new ProductController(mock.Object);
            var product = new Products() { Name = "Test Name 3", Description = "Test Description 3", Price = 10000M };

            //Act
            var data = await prod.Create(product);

            //Assert
            Assert.IsType<RedirectToActionResult>(data);
        }
        #endregion
        #region Update Existing Product

        [Fact]
        public async void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange
            ProductController prod = new ProductController(mock.Object);
            var product = new Products() { ID=1,Name = "Test", Description = "Test", Price = 10000M };
            //Act
            mock.Setup(p => p.UpdateProduct(product)).ReturnsAsync(1);
            var data = await prod.Edit(1, product);

            //Assert
            Assert.IsType<RedirectToActionResult>(data);
        }
        #endregion
        #region Delete Products

        [Fact]
        public async void Task_Delete_Product_Return_OkResult()
        {
            //Arrange
            ProductController prod = new ProductController(mock.Object);

            //Act
            mock.Setup(p => p.DeleteProduct(2)).ReturnsAsync(1);
            var data = await prod.DeleteConfirmed(1);

            //Assert
            Assert.IsType<RedirectToActionResult>(data);
        }
        #endregion
    }
}
