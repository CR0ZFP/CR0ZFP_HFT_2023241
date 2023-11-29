using System;
using System.Collections.Generic;
using System.Linq;
using CR0ZFP_HFT_202324.Models;
using CR0ZFP_HFT_202324.Repository;
using CR0ZFP_HFT_2023241.Logic;
using Moq;
using NUnit.Framework;

namespace CR0ZFP_HFT_202324.Test
{
    [TestFixture]
    public class CustomerLogicTester
    {
        CustomerLogic logic;
        Mock<IRepository<Customer>> mockCustomerRepository;

        [SetUp]
        public void Init()
        {
            mockCustomerRepository = new Mock<IRepository<Customer>>();
            mockCustomerRepository.Setup(x => x.ReadAll()).Returns(new List<Customer>()
            {
                new Customer("10#Gaben#gabengod@gmail.com#Fejér#23"),
                new Customer("4#Gorgo#gorgo@gmail.com#Bács-kiskun#19"),
                new Customer("5#Krisáni#DrunkenKrisa@gmail.com#Fejér#18"),

            }.AsQueryable());
            logic = new CustomerLogic(mockCustomerRepository.Object);
        }

        [Test]
        public void AvarageAgeOfCustomerTest()
        {
            double? avg = logic.AvarageAgeOfCustomers();
            Assert.That(avg, Is.EqualTo(20));
        }

        [Test]
        public void GetCustomersByCountyTest ()
        {
            var customers = logic.GetCustomersByCounty("Fejér").ToList<Customer>();
            List<Customer> result = new List<Customer>()
            {
                new Customer("10#Gaben#gabengod@gmail.com#Fejér#23"),
                new Customer("5#Krisáni#DrunkenKrisa@gmail.com#Fejér#18"),
            };
            Assert.AreEqual(customers, result);
        }

        [Test]
        public void CustomerCreateTest()
        {
            var customer = new Customer("5#Krisáni#DrunkenKrisa@gmail.com#Fejér#18");

            logic.Create(customer);

            mockCustomerRepository.Verify(x => x.Create(customer), Times.Once());
        }

        [Test]
        public void InvalidCustomerCreateTest()
        {
            var customer = new Customer("5#Krisáni#DrunkenKrisa@gmail.com#Fejér#17");

            try
            {
                logic.Create(customer);
            }catch(Exception e)
            {

            }

            mockCustomerRepository.Verify(x => x.Create(customer), Times.Never());
        }
    }
}
