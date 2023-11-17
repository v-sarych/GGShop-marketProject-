using Microsoft.EntityFrameworkCore;
using ShopDb;
using ShopDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Common
{
    public static class TestShopDbContextFactory
    {
        public static ShopDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ShopDbContext(options);
            context.Database.EnsureCreated();

            _addDataToDb(context);

            return context;
        }

        public static void Destroy(ShopDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static void _addDataToDb(ShopDbContext context)
        {
            context.PackageSizes.AddRange(new PackageSize
            {
                Id = 1,
                Width = 10,
                Height = 10,
                Length = 10,
            }, new PackageSize
            {
                Id = 2,
                Width = 30,
                Height = 30,
                Length = 30,
            }, new PackageSize
            {
                Id = 3,
                Width = 40,
                Height = 30,
                Length = 30,
            });

            context.Tags.AddRange(new Tag
            {
                Id = 1,
                Name = "tag test",
            }, new Tag
            {
                Id = 2,
                Name = "tag2 test",
            });

            context.Products.AddRange(new Product
            {
                Id = 1,
                Description = "Test",
                CanBeFound = true,
                Name = "product1",
                AvailabilitisOfProduct = new List<AvailabilityOfProduct>
                {
                    new AvailabilityOfProduct()
                    {
                        Sku = "product1_s",
                        Size = "s",
                        Cost = 1000,
                        Count = 100,
                        Weight = 100,
                        PackageSizeId = 1,
                    }
                }
            }, new Product
            {
                Id = 2,
                Description = "pr2 test data",
                CanBeFound = true,
                Name = "product2",
                AvailabilitisOfProduct = new List<AvailabilityOfProduct>
                {
                    new AvailabilityOfProduct()
                    {
                        Sku = "product2_s",
                        Size = "s",
                        Cost = 1000,
                        Count = 100,
                        Weight = 200,
                        PackageSizeId = 1,
                    }, new AvailabilityOfProduct()
                    {
                        Sku = "product2_m",
                        Size = "m",
                        Cost = 10,
                        Count = 0,
                        Weight = 100,
                        PackageSizeId = 1,
                    }, new AvailabilityOfProduct()
                    {
                        Sku = "product2_xl",
                        Size = "xl",
                        Cost = 10,
                        Count = 10,
                        Weight = 300,
                        PackageSizeId = 2,
                    }
                }
            }, new Product
            {
                Id = 3,
                Description = "pr3 test data",
                CanBeFound = true,
                Name = "product3",
                AvailabilitisOfProduct = new List<AvailabilityOfProduct>
                {
                    new AvailabilityOfProduct()
                    {
                        Sku = "product3_s",
                        Size = "s",
                        Cost = 1000,
                        Count = 100,
                        Weight = 10,
                        PackageSizeId = 1,
                    }, new AvailabilityOfProduct()
                    {
                        Sku = "product3_m",
                        Size = "m",
                        Cost = 10,
                        Count = 3,
                        Weight = 200,
                        PackageSizeId = 1,
                    }, new AvailabilityOfProduct()
                    {
                        Sku = "product3_xl",
                        Size = "xl",
                        Cost = 10,
                        Count = 10,
                        Weight = 500,
                        PackageSizeId = 2,
                    }
                }
            }, new Product
            {
                Id = 4,
                Description = "Test",
                CanBeFound = false,
                Name = "product4",
                AvailabilitisOfProduct = new List<AvailabilityOfProduct>
                {
                    new AvailabilityOfProduct()
                    {
                        Sku = "product4_s",
                        Size = "s",
                        Cost = 1000,
                        Count = 100,
                        Weight = 100,
                        PackageSizeId = 1,
                    }
                }
            }, new Product
            {
                Id = 5,
                Description = "Test",
                CanBeFound = true,
                Name = "product5",
                AvailabilitisOfProduct = new List<AvailabilityOfProduct>
                {
                    new AvailabilityOfProduct()
                    {
                        Sku = "product5_s",
                        Size = "s",
                        Cost = 1000,
                        Count = 100,
                        Weight = 100,
                        PackageSizeId = 1,
                    }
                }
            });

            context.ProductTags.AddRange(new ProductTag
            {
                TagId = 1,
                ProductId = 1,
            }, new ProductTag
            {
                TagId = 1,
                ProductId = 2,
            }, new ProductTag
            {
                TagId = 2,
                ProductId = 2,
            }, new ProductTag
            {
                TagId = 1,
                ProductId = 3,
            }, new ProductTag
            {
                TagId = 1,
                ProductId = 4,
            }, new ProductTag
            {
                TagId = 2,
                ProductId = 5,
            });


            context.Users.AddRange(new User()
            {
                Id = 1,
                Name = "Test1",
                Password = "password",
                Role = Roles.User,
                PhoneNumber = "1234567890",
            }, new User()
            {
                Id = 2,
                Name = "test2",
                Password = "ddf",
                Role = Roles.User,
                PhoneNumber = "12342323",
                UserShoppingCartItems = new List<UserShoppingCartItem>()
                {
                    new UserShoppingCartItem()
                    {
                        Sku = "product1_s",
                        Count = 1
                    },new UserShoppingCartItem()
                    {
                        Sku = "product2_m",
                        Count = 1
                    }, new UserShoppingCartItem()
                    {
                        Sku = "product3_xl",
                        Count = 5
                    }
                }
            }, new User()
            {
                Id = 3,
                Name = "test3",
                Password = "123456",
                Role = Roles.User,
                PhoneNumber = "89251906041",
                UserShoppingCartItems = new List<UserShoppingCartItem>()
                {
                    new UserShoppingCartItem()
                    {
                        Sku = "product3_m",
                        Count = 5
                    }
                }
            });

            context.Orders.AddRange(new Order()
            {
                Id = Guid.NewGuid(),
                DateOfCreation = DateTime.Now,
                AdditionalOrderInfo = "s",
                Status = OrderStatuses.Created,
                UserId = 2,
                IsPaidFor = false,
                Type = OrderTypes.Order,
                Cost = 100,
                WebHookKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Count = 1,
                        Sku = "product1_s",
                        Cost = 100,
                        ProductId = 1,
                    }
                }
            });


            context.SaveChanges();
        }
    }
}
