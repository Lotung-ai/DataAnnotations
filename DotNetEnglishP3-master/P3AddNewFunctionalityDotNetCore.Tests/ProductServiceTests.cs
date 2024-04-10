using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        /// <summary>
        /// Moq for testing Product Name and if it is no Null
        /// </summary>

        [Fact]
        public void ProductNameNullTest()
        {
            // Arrange
            Mock<ICart> mockCart = new Mock<ICart>();
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            ProductService productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            ProductViewModel productNameNull = new ProductViewModel { Id = 1, Name = null, Price = 10.99.ToString(), Stock = 50.ToString() };
            ProductViewModel product = new ProductViewModel { Id = 2, Name = "Product 2", Price = 20.99.ToString(), Stock = 20.ToString() };
            mockLocalizer.Setup(m => m["MissingName"]).Returns(new LocalizedString("MissingName", "Missing name error message"));

            // Act
            List<string> resultNameNull = productService.CheckProductModelErrors(productNameNull);
            List<string> result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(result);
            Assert.NotEmpty(resultNameNull);
            Assert.Contains("Missing name error message", resultNameNull);
        }

        /// <summary>
        /// Test if Product Price is Decimal
        /// </summary>
        [Fact]
        public void ProductPriceNullTest()
        {
            // Arrange
            Mock<ICart> mockCart = new Mock<ICart>();
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            ProductService productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            ProductViewModel productPriceNull = new ProductViewModel { Id = 1, Name = "Product 1", Price = null, Stock = 50.ToString() };
            ProductViewModel product = new ProductViewModel { Id = 2, Name = "Product 2", Price = 20.99.ToString(), Stock = 20.ToString() };
            mockLocalizer.Setup(m => m["MissingPrice"]).Returns(new LocalizedString("MissingPrice", "Missing price error message"));

            // Act
            List<string> resultPrioceNull = productService.CheckProductModelErrors(productPriceNull);
            List<string> result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(result);
            Assert.NotEmpty(resultPrioceNull);
            Assert.Contains("Missing price error message", resultPrioceNull);
        }
        /// <summary>
        /// Test if Product Priceis Double type
        /// </summary>
        [Fact]
        public void ProductPriceIsNotDoubleTypeTest()
        {
            // Arrange
            Mock<ICart> mockCart = new Mock<ICart>();
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            ProductService productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            ProductViewModel productPriceIsNotDoubleType = new ProductViewModel { Id = 1, Name = "Product 1", Price = "NotNumber", Stock = 50.ToString() };
            ProductViewModel product = new ProductViewModel { Id = 2, Name = "Product 2", Price = 20.99.ToString(), Stock = 20.ToString() };
            mockLocalizer.Setup(m => m["PriceNotANumber"]).Returns(new LocalizedString("PriceNotANumber", "Price is not a number error message"));

            // Act
            List<string> resultPrioceIsNotDoubleType = productService.CheckProductModelErrors(productPriceIsNotDoubleType);
            List<string> result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(result);
            Assert.NotEmpty(resultPrioceIsNotDoubleType);
            Assert.Contains("Price is not a number error message", resultPrioceIsNotDoubleType);
        }

        /// <summary>
        /// Test if Product Price > 0
        /// </summary>
        [Fact]
        public void ProductPriceIsNotPositiveTest()
        {
            // Arrange
            Mock<ICart> mockCart = new Mock<ICart>();
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            ProductService productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            ProductViewModel productPriceIsNotPositive = new ProductViewModel { Id = 1, Name = "Product 1", Price = (-10).ToString(), Stock = 50.ToString() };
            ProductViewModel product = new ProductViewModel { Id = 2, Name = "Product 2", Price = 20.99.ToString(), Stock = 20.ToString() };
            mockLocalizer.Setup(m => m["PriceNotGreaterThanZero"]).Returns(new LocalizedString("PriceNotGreaterThanZero", "Price is not positive error message"));

            // Act
            List<string> resultPriceIsNotPositive = productService.CheckProductModelErrors(productPriceIsNotPositive);
            List<string> result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(result);
            Assert.NotEmpty(resultPriceIsNotPositive);
            Assert.Contains("Price is not positive error message", resultPriceIsNotPositive);
        }

        /// <summary>
        /// Test if Product.Quantity is not null
        /// </summary>
        [Fact]
        public void ProductQuantityNullTest()
        {
            // Arrange
            Mock<ICart> mockCart = new Mock<ICart>();
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            ProductService productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            ProductViewModel productQuantityNull = new ProductViewModel { Id = 1, Name = "Product 1", Price = 10.99.ToString(), Stock = null };
            ProductViewModel product = new ProductViewModel { Id = 2, Name = "Product 2", Price = 20.99.ToString(), Stock = 20.ToString() };
            mockLocalizer.Setup(m => m["MissingQuantity"]).Returns(new LocalizedString("MissingQuantity", "Missing quantity error message"));

            // Act
            List<string> resultQuantityNull = productService.CheckProductModelErrors(productQuantityNull);
            List<string> result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(result);
            Assert.NotEmpty(resultQuantityNull);
            Assert.Contains("Missing quantity error message", resultQuantityNull);
        }

        /// <summary>
        /// Test if Product.Quantity is Integer
        /// </summary>
        [Fact]
        public void ProductQuantityIsNotIntegerTest()
        {
            // Arrange
            Mock<ICart> mockCart = new Mock<ICart>();
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            ProductService productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            ProductViewModel productQuantityIsNotInteger = new ProductViewModel { Id = 1, Name = "Product 1", Price = 10.99.ToString(), Stock = 1.5.ToString() };
            ProductViewModel product = new ProductViewModel { Id = 2, Name = "Product 2", Price = 20.99.ToString(), Stock = 20.ToString() };
            mockLocalizer.Setup(m => m["StockNotAnInteger"]).Returns(new LocalizedString("StockNotAnInteger", "Stock is not an integer error message"));

            // Act
            List<string> resultQuantityIsNotInteger = productService.CheckProductModelErrors(productQuantityIsNotInteger);
            List<string> result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(result);
            Assert.NotEmpty(resultQuantityIsNotInteger);
            Assert.Contains("Stock is not an integer error message", resultQuantityIsNotInteger);
        }

        /// <summary>
        /// Test if Product.Quantity > 0
        /// </summary>
        [Fact]
        public void ProductQuantityIsNotPositiveTest()
        {
            // Arrange
            Mock<ICart> mockCart = new Mock<ICart>();
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            ProductService productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            ProductViewModel productQuantityIsNotPositive = new ProductViewModel { Id = 1, Name = "Product 1", Price = 10.99.ToString(), Stock = (-15).ToString() };
            ProductViewModel product = new ProductViewModel { Id = 2, Name = "Product 2", Price = 20.99.ToString(), Stock = 20.ToString() };
            mockLocalizer.Setup(m => m["StockNotGreaterThanZero"]).Returns(new LocalizedString("StockNotGreaterThanZero", "Stock is not positive error message"));

            // Act
            List<string> resultQuantityIsNotPositive = productService.CheckProductModelErrors(productQuantityIsNotPositive);
            List<string> result = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(result);
            Assert.NotEmpty(resultQuantityIsNotPositive);
            Assert.Contains("Stock is not positive error message", resultQuantityIsNotPositive);

        }
        /// <summary>
        /// Verify if the product is adding in the inventory
        /// </summary>
        [Fact]
        public void SaveProductTest_ShouldBeContainsNewProduct()
        {
            // Arrange
            Mock<ICart> mockCart = new Mock<ICart>();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            // Create options for in-memory database
            var options = new DbContextOptionsBuilder<P3Referential>()
               .UseInMemoryDatabase(databaseName: "data name")
               .Options;

            // Create real context for ProductRepository
            P3Referential context = new P3Referential(options, null);
            {
                // Seed the in-memory database with some initial products
                context.Product.AddRange(new List<Product>
                {
                    new Product { Id = 1, Name = "Product 1", Price = 10.99, Quantity = 50 },
                    new Product { Id = 2, Name = "Product 2", Price = 24.99, Quantity = 30 },
                    new Product { Id = 3, Name = "Product 3", Price = 54.99, Quantity = 20 }
                });
                context.SaveChanges();

                // Create real ProductRepository using the context
                ProductRepository productRepository = new ProductRepository(context);

                ProductService productService = new ProductService(mockCart.Object, productRepository, mockOrderRepository.Object, mockLocalizer.Object);

                ProductViewModel newProductViewModel = new ProductViewModel
                {
                    Name = "New Product",
                    Price = 19.99.ToString(),
                    Stock = 50.ToString(),
                    Description = "This is a new product",
                    Details = "Some details about the new product"
                };

                // Act
                productService.SaveProduct(newProductViewModel);

                // Instance the list after the add the product
                List<ProductViewModel> updatedProducts = productService.GetAllProductsViewModel();

                // Assert
                // Verify if the product is in the inventory
                Assert.Contains(updatedProducts, p => p.Name == "New Product");

                // Check if the originals products is the inventory after the update
                Assert.Contains(updatedProducts, p => p.Name == "Product 1");
                Assert.Contains(updatedProducts, p => p.Name == "Product 2");
                Assert.Contains(updatedProducts, p => p.Name == "Product 3");
            }
        }


        [Fact]
        public void AddNewProductInTheCart()
        {
            // Arrange
            Cart cart = new Cart();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            // Get options in the in-memory database "data name" instanced in SaveProductTest_ShouldBeContainsNewProduct()
            var options = new DbContextOptionsBuilder<P3Referential>()
               .UseInMemoryDatabase(databaseName: "data name")
               .Options;

            // Create real context for ProductRepository
            P3Referential context = new P3Referential(options, null);
            {
                // Create real ProductRepository using the context
                ProductRepository productRepository = new ProductRepository(context);

                ProductService productService = new ProductService(cart, productRepository, mockOrderRepository.Object, mockLocalizer.Object);

                // Instance the list 

                List<Product> Products = productService.GetAllProducts();



                // Act
                cart.AddItem(Products.First(p => p.Name == "New Product"), 1);
                cart.AddItem(Products.First(p => p.Name == "Product 1"), 1);


                // Assert
                // Verify if the product is in the inventory 
                Assert.Contains(Products, p => p.Name == "New Product");

                // Verify if the products are in the cart
                Assert.NotEmpty(cart.Lines);
                Assert.Equal(2, cart.Lines.Count());

                //If the assert passed then item removed was "New Product"
                cart.RemoveLine(Products.First(p => p.Name == "New Product"));
                Assert.Equal(1, cart.Lines.Count());
            }
        }

        /// <summary>
        /// Verify if the product is adding in the inventory
        /// </summary>
        [Fact]
        public void DeleteProductTest_ShouldBeDeleteProductById()
        {
            // Arrange
            Cart cart = new Cart();
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            Mock<IStringLocalizer<ProductService>> mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            // Get options in the in-memory database "data name 2"
            var options = new DbContextOptionsBuilder<P3Referential>()
               .UseInMemoryDatabase(databaseName: "data name 2")
               .Options;

            // Create real context for ProductRepository
            P3Referential context = new P3Referential(options, null);
            {
                // Seed the in-memory database with some initial products
                context.Product.AddRange(new List<Product>
                {
                    new Product { Id = 1, Name = "Product 1", Price = 10.99, Quantity = 50 },
                    new Product { Id = 2, Name = "Product 2", Price = 24.99, Quantity = 30 },
                    new Product { Id = 3, Name = "Product 3", Price = 54.99, Quantity = 20 }
                });
                context.SaveChanges();
                // Create real ProductRepository using the context
                ProductRepository productRepository = new ProductRepository(context);

                ProductService productService = new ProductService(cart, productRepository, mockOrderRepository.Object, mockLocalizer.Object);

                // Instance the list 
                List<Product> Products = productService.GetAllProducts();

                // Verify if the product is in the inventory before delete
                Assert.Contains(Products, p => p.Name == "Product 2");
                cart.AddItem(Products.First(p => p.Name == "Product 2"), 1);
                Assert.NotEmpty(cart.Lines);

                // Act
                productService.DeleteProduct(2);

                // Instance the list after the delete
                Products = productService.GetAllProducts();


                // Assert
                // Verify if the product is in the inventory
                Assert.Empty(cart.Lines);
                Assert.Equal(2, Products.Count);

                // Check if the originals products is the inventory after the update
                Assert.Contains(Products, p => p.Name == "Product 1");
                Assert.Contains(Products, p => p.Name == "Product 3");

            }
        }
    }
}