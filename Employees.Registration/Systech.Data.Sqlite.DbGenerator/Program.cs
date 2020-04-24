using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Abstraction; 
using Data.EntityFramework;

namespace Data.Sqlite.DbGenerator
{
    class Program
    {
        private static async Task Main()
        { 
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSqliteContextFor<IDataContext, DataContext>("Data Source=TestEmp.db");
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var currentColor = Console.ForegroundColor;

            try
            {
                using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                await using var dataContext = scope.ServiceProvider.GetRequiredService<IDataContext>();
                var result = await dataContext.CreateBackendAsync().ConfigureAwait(false);
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Success!"); 
                    await dataContext.SaveAsync().ConfigureAwait(false);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Database exists!");
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Exception: {e.StackTrace}");
            }

            Console.ForegroundColor = currentColor;
        }
    }
}
