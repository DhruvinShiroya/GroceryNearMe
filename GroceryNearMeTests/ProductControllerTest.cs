using GroceryNearMe.Controllers;
using GroceryNearMe.Data;
using GroceryNearMe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Net;

namespace GroceryNearMeTests

{
    [TestClass]
    public class ProductControllerTest
    {

        private ProductsController _controller;
        private ApplicationDbContext _context;

        private Category _category;
        private Store _store;
        private List<Product> _products = new List<Product>();
        private List<Review> _review = new List<Review>();


        // this is ran when the class is created
        [TestInitialize]
        public void TestIntialize()
        {
            // mock db context for test class only

            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;


            _context = new ApplicationDbContext(dbOptions);

            

            // Create a mock Store 
            _store = new Store
            {
                Id = 123,
                Name = "Test Store 1",
                AddressId = 1000,
                CompanyId = 1000
            };

            // add mock store to db

            _context.Stores.Add(_store);


            // Create Mock category
            _category = new Category
            {
                Id = 2000,
                Name = "Test Category",
            };

            // add mock category to the db

            _context.Categories.Add(_category);

            // create a mock mock reviews for the prodcuts

            _review.Add(new Review
            {
                ProductID = 1000,
                ReviewId = 1000,
                UpVote = 0,
                DownVote = 0,
                Rating = 4,
                UserName = "testUser1"
            });

            _review.Add(new Review
            {
                ProductID = 2000,
                ReviewId = 2000,
                UpVote = 0,
                DownVote = 0,
                Rating = 4,
                UserName = "testUser2"
            });

            foreach (var item in _review)
            {
                _context.Reviews.Add(item);
            }

            // create few Mock products and add to the list of product

            _products.Add(new Product
            {
                Id = 123,
                Name = "Test Prodct 1",
                Price = (decimal) 12.05,
                QuantityInKG = 1,
                CategoryId = 2000,
                StoreId = 123,
                Description = "Description 1"

            });

            _products.Add(new Product
            {
                Id = 124,
                Name = "Test Prodct 2",
                Price = (decimal)22.05,
                QuantityInKG = 2,
                CategoryId = 2000,
                StoreId = 123,
                Description = "Description 1",

            });

            _products.Add(new Product
            {
                Id = 125,
                Name = "Test Prodct 3",
                Price = (decimal)5.05,
                QuantityInKG = 1,
                CategoryId = 2000,
                StoreId = 123,
                Description = "Description 2",

            });

            // add product to the mock db

            foreach(var item in _products)
            {
                _context.Products.Add(item);
            }

            // save changes to the mock db context

            _context.SaveChanges();

            //give the mock db context to the product controller
            _controller = new ProductsController(_context);
        }

      
        
        [TestMethod]
        public async Task IndexRetrunProductList()
        {

            // Arange
            // setup your application state Create vars and create object


            // Act 
            // Running a function or method to be tested
            // and gettring results

            var result = (ViewResult) await _controller.Index();

            var resultList = (List<Product>) result.Model;

            CollectionAssert.AreEqual(_products.OrderBy(p => p.Name).ToList(), resultList);
        }

        [TestMethod]
        public async Task getDetailsIdIsNull()
        {
            // arange

            // act
            // pass null value 
            var result = (NotFoundResult) await _controller.Details(null);

            // assert 
            // if return value is not found 
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result,typeof(NotFoundResult));

        }


        [TestMethod]
        public async Task getDettailsProdcutListIsEmpty()
        {
            //
            // this is making code running slow 
            //_context.Products = null; from 3.3s to 1.1s
            //_context.SaveChanges();
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;


              _context = new ApplicationDbContext(dbOptions);
              _controller = new ProductsController(_context);

            // act
            // pass null value 
            var result = (NotFoundResult)await _controller.Details(null);

            // assert 
            // if return value is not found 
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task DetailsProductsNotExist()
        {
            // arrange
            //_context.SaveChanges

            var idNotExist = -1;
           
            // act
            // pass null value 
            var result = (NotFoundResult)await _controller.Details(idNotExist);

            // assert 
            // if return value is not found 
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DetailsIfProductsExist()
        {
            // arrange
            //_context.SaveChanges

            var idExist = 124;

            // act
            // pass null value 
            var result = (ViewResult) await _controller.Details(idExist);

            // assert 
            // if return value is not found 
            
            Assert.AreEqual(result.Model, _products.Find(p => p.Id == idExist));
        }





    }
}