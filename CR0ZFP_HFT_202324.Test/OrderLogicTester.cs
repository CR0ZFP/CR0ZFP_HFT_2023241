using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CR0ZFP_HFT_202324.Models;
using CR0ZFP_HFT_202324.Repository;
using CR0ZFP_HFT_2023241.Logic;
using Moq;
using NUnit.Framework;

namespace CR0ZFP_HFT_202324.Test
{
    [TestFixture]
    public class OrderLogicTester
    {
        OrderLogic logic;
        ProductLogic productLogic;
        Mock<IRepository<Order>> mockOrderRepository;
        Mock<IRepository<Product>> mockProductRepository;

        [SetUp]
        public void Init ()
        {
            mockProductRepository = new Mock<IRepository<Product>> ();
            mockProductRepository.Setup(x => x.ReadAll()).Returns(new List<Product>
            {
                new Product("100#10#Soproni#0,5#450"),
                new Product("101#10#Kőbányai#0,5#300"),
                new Product("102#10#Peroni#0,5#600"),

                new Product("103#20#Meggy#0,7#3000"),
                new Product("104#20#Barack#0,7#4500"),

                new Product("105#20#Milka#120#500"),
                new Product("106#20#Boci#100#450"),

                new Product("107#30#Haribo#80#450"),
                new Product("108#30#Damla#80#550"),

                new Product("109#40#Lays#120#790"),
                new Product("110#40#Chio#140#990"),


            }.AsQueryable());
            mockOrderRepository = new Mock<IRepository<Order>>();
            mockOrderRepository.Setup(x=>x.ReadAll()).Returns(new List<Order>
            {
                new Order("10#100#2023.11.01"),
                new Order("20#100#2023.11.10"),
                new Order("30#100#2023.10.01"),
                new Order("40#100#2023.10.01"),
            }.AsQueryable());

            logic = new OrderLogic(mockOrderRepository.Object);
            productLogic = new ProductLogic(mockProductRepository.Object);
        }

        //ORDERS

        [Test]
        public void GetOrdersbyDateTest1 ()
        {
            var orders = logic.GetOrdersByDate(new DateTime(2023, 10, 1),1);
            List<Order> result = new List<Order>
            {
                new Order("30#100#2023.10.01"),
                new Order("40#100#2023.10.01"),
            };
            Assert.AreEqual(orders, result);
        }
        [Test]
        public void GetOrdersbyDateTest2()
        {
            var orders = logic.GetOrdersByDate(new DateTime(2023, 10, 29), 2);
            List<Order> result = new List<Order>
            {
                new Order("10#100#2023.11.01"),
                new Order("20#100#2023.11.10"),
            };
            Assert.AreEqual(orders, result);
        }
        [Test]
        public void OrderCreateTest()
        {
            var order = new Order("20#100#2023.11.10");

            logic.Create(order);

            mockOrderRepository.Verify(x => x.Create(order), Times.Once());
        }

        //PRODUCTS

        [Test]
        public void getAvaragePriceTest ()
        {
            double actual = productLogic.GetAvaragePrice();
            double result = 1144;
            Assert.AreEqual(actual, result);
        }
        [Test]
        public void MostValuableProductTest()
        {
            Product actual = productLogic.MostValuableProduct();
            Product result = new Product("104#20#Barack#0,7#4500");
            Assert.AreEqual(actual, result);
        }

        [Test]
        public void ProductCreateTest()
        {
            var product = new Product("104#20#Barack#0,7#4500");

            productLogic.Create(product);

            mockProductRepository.Verify(x => x.Create(product), Times.Once());
        }

        [Test]
        public void InvalidProductCreateTest()
        {
            var product = new Product("104#20##0,7#4500");

            try
            {
                productLogic.Create(product);
            }
            catch (Exception e)
            {

            }

            mockProductRepository.Verify(x => x.Create(product), Times.Never());
        }



        [Test]
        public void GetFullPriceTest()
        {
            double expected = logic.GetFullPrice(10);
            double actual = 1350;

            Assert.AreEqual(expected, actual);
        }
    }
}
