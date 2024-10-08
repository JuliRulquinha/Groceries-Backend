
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Groceries
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<SqlConnection>((_) => new SqlConnection("Server=localhost;Database=Groceries;Trusted_Connection=True;"));
            builder.Services.AddSingleton<IGroceriesRepository, InMemoryGroceriesRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            //Product product = new Product();
            //product.name = "Test2";
            //product.imgUrl = "not even an image";
            //product.description = "Description";
            //product.price = 5000;
            //product.quantity = 1;
            //product.categoryId = 1;

            //SqlConnection connection = new SqlConnection();

            //GroceriesRepository groceriesRepository = new GroceriesRepository(connection);
            ////groceriesRepository.DeleteById(5);
            //groceriesRepository.UpdateById(6, product);

            //groceriesRepository.TestConnection();

            //Category category = new Category();
            //var categoryRepository = new CategoryRepository();

            //IEnumerable<string> Fruits = new List<string>{"apple","banana","cherry","date","elderberry","fig","grape","honeydew","kiwi","lemon","mango","nectarine","orange","papaya","quince","raspberry","strawberry","tangerine","watermelon","zucchini" };

            //var firstFruit = Fruits.Average(f => f.Length);

            //foreach (var f in firstFruit)
            //{
            //Console.WriteLine(firstFruit); 
            // }

            //var littleFruits =  from f in Fruits where f.Length < 4 select f;

            //foreach ( var little in littleFruits ) { Console.WriteLine( little ); }

            //var bigFruits = (from f in Fruits where f.Length > 4 select f).ToList().First();

            var dB = new MyFirstContext();

            //var product = new Product()
            //{

            //    Name = "SecondTestEF",
            //    CategoryId = 1,
            //    ImgUrl = "sdgwerege",
            //    Price = 53.5m
            //};

            //dB.Products.Add(product);
            //dB.SaveChanges();

            //var products = dB.Products.ToList();

            //foreach (var p in products) 
            //{
            //    Console.WriteLine(p.Name);
            //}

            var firstProduct = dB.Products.Where(p => p.Price > 0 && p.Price<100).First();
            Console.WriteLine(firstProduct.Price);

        }
    }
}
