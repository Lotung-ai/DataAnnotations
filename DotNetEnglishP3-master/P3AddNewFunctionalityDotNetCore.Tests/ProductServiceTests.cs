using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models;
using System.Collections.Generic;
using Xunit;

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
    }
}