
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

            Products product = new Products();
            product.name = "Test2";
            product.imgUrl = "not even an image";
            product.Description = "Description";
            product.price = 5000;
            product.quantity = 1;
            product.categoryId = 1;
            

            GroceriesRepository groceriesRepository = new GroceriesRepository();
            //groceriesRepository.DeleteById(5);
            groceriesRepository.UpdateById(6, product);

            groceriesRepository.TestConnection();

        }
    }
}
