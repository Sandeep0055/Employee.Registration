using Data.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.Registration.HostedServices
{
 
    /// <summary>
    /// Sandeep More 24/04/2020\
    /// To check Database is Exist or not
    /// if Not then it creates
    /// </summary>
    public class EnsureHostedService : BackgroundService
    {
        private readonly ILogger<EnsureHostedService> _logger;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EnsureHostedService(ILogger<EnsureHostedService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IDataContext>();
            _logger.LogInformation("Database Creating");
            if (context.CreateBackendAsync(stoppingToken)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult())
            {

                var path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ??
                           throw new InvalidOperationException("file path does not exist");

                if (context.ExecuteCommands(Path.Combine(path,
                        "FileInsert.sql"), ";", cancellationToken: stoppingToken)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult())
                {
                    _logger.LogInformation("master script exceeded successfully");
                }
                else
                {
                    throw new ApplicationException("failed to master script executing");
                }
            }
            else
            {
                _logger.LogInformation("database is already exists");
            }

            _logger.LogInformation("all task completed");
            return Task.CompletedTask;
        }


    }
}
