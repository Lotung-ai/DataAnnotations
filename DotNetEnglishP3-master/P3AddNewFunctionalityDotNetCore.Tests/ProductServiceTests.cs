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
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        /// <summary>
        /// Moq for testing Product Name and if it is no Null
        /// </summary>

        [Fact]
        public void Name_Required()
        {
            // Arrange
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel { Id = 1, Name = null, Price = 10.ToString(), Stock = 50.ToString() };

            // Act
            var modelErrors = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Veuillez saisir un nom", modelErrors);
        }

        /// <summary>
        /// Test if Product Price is Decimal
        /// </summary>
        [Fact]
        public void Price_Required()
        {
            // Arrange
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel { Id = 1, Name = "Product 1", Price = null, Stock = 50.ToString() };

            // Act
            var modelErrors = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Veuillez saisir un prix", modelErrors);
        }

        /// <summary>
        /// Test if Product Priceis Double type
        /// </summary>
        [Fact]
        public void Price_RegularExpression()
        {
            // Arrange
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel { Id = 1, Name = "Product 1", Price = "Price", Stock = 50.ToString() };

            // Act
            var modelErrors = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("La valeur saisie pour le prix doit être un nombre", modelErrors);
        }

        /// <summary>
        /// Test if Product Price > 0
        /// </summary>
        [Fact]
        public void Price_Range()
        {
            // Arrange
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var priceNeg = -10;
            var productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel { Id = 1, Name = "Product 1", Price = priceNeg.ToString(), Stock = 50.ToString() };

            // Act
            var modelErrors = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("La prix doit être supérieur à zéro", modelErrors);
        }

        /// <summary>
        /// Test if Product.Quantity is not null
        /// </summary>
        [Fact]
        public void Stock_Required()
        {
            // Arrange
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel { Id = 1, Name = "Product 1", Price = 10.ToString(), Stock = null };

            // Act
            var modelErrors = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Veuillez saisir un stock", modelErrors);
        }

        /// <summary>
        /// Test if Product.Quantity is Integer
        /// </summary>
        [Fact]
        public void Stock_RegularExpression()
        {
            // Arrange
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<ProductService>>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel { Id = 1, Name = "Product 1", Price = 10.ToString(), Stock = "Stock" };

            // Act
            var modelErrors = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("La valeur saisie pour le stock doit être un entier", modelErrors);
        }

        /// <summary>
        /// Test if Product.Quantity > 0
        /// </summary>
        [Fact]
        public void Stock_Range()
        {
            // Arrange
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();
            var mockLocalizer = new Mock<IStringLocalizer<ProductService>>();
            var stock = -50;

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object, mockOrderRepository.Object, mockLocalizer.Object);

            var product = new ProductViewModel { Id = 1, Name = "Product 1", Price = 10.ToString(), Stock = stock.ToString() };

            // Act
            var modelErrors = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("La stock doit être supérieure à zéro", modelErrors);
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