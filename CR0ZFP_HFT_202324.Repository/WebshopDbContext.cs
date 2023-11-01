using System;
using CR0ZFP_HFT_202324.Models;
using Microsoft.EntityFrameworkCore;

namespace CR0ZFP_HFT_202324.Repository
{
    public class WebshopDbContext : DbContext
    {
        public WebshopDbContext()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Webshop.mdf"";Integrated Security=True; MultipleActiveResultsSets=true" ;
                builder.UseLazyLoadingProxies()
                    .UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(order => order.HasOne(order => order.Customer)
            .WithMany(customer => customer.Orders)
            .HasForeignKey(order => order.CustomerID).OnDelete(DeleteBehavior.SetNull));

            modelBuilder.Entity<Product>().HasOne(p => p.Order)
                .WithMany(o => o.Products).HasForeignKey(p => p.OrderID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Customer>().HasData(new Customer[]
            {
                new Customer("1#Fred#fred100@gmail.com#Vas"),
                new Customer("2#Steve#steve100@gmail.com#Zala"),
                new Customer("3#George#georg100@gmail.com#Baranya"),
                new Customer("4#John#John100@gmail.com#Tolna")
            });

            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product("100#1000#Apple#0.5#2.5"),
                new Product("101#1000#Pear#0.3#3.4"),
                new Product("102#1000#Orange#1#5"),
                new Product("103#1000#Banana#5#20"),

                new Product("104#1001#Pen#0.3#4"),
                new Product("105#1001#Ruler#0.4#5.5"),
                new Product("106#1001#Eraser#0.2#1.5"),
                new Product("107#1001#Notebook#0.4#3"),

                new Product("108#1002#Bread#1.5#3"),
                new Product("109#1002#Muffin#0.3#5"),
                new Product("110#1002#Doughnut#0.5#7"),


            });

            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                new Order("1000#100#2023.11.01"),
                new Order("1001#102#2023.10.29"),
                new Order("1002#103#2023.10.18")

            });
            

            
        }


    }
}
