// See https://aka.ms/new-console-template for more information
using consolebdd.Data;
using Microsoft.Extensions.Configuration;

namespace consolebdd.App
{
    public class Program
    {
        public static IConfigurationRoot? Configuration { get; set; }

        public static void Main(string[] args)
        {
            ReadConfiguration();

            using (var db = new SchoolContext())
            {
                Console.WriteLine("Creating database...\n");
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("Seeding database...\n");
            }
        }

        private static void ReadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            ConnectionStrings.DefaultConnection = Configuration["DefaultConnection"];

            Console.WriteLine("Configuration\n");
            Console.WriteLine($@"connectionString (defaultConnection) = ""{ConnectionStrings.DefaultConnection}""");
            Console.WriteLine();
        }
    }
}