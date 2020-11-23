using System;
using Microsoft.Extensions.DependencyInjection;

namespace Seller.Shared.Messages
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data.Models;
    using Hangfire;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;

    public class MessagesHostedService : IHostedService
    {
        private readonly IRecurringJobManager recurringJob;
        private readonly IServiceProvider services;
        private readonly IBus publisher;

        public MessagesHostedService(
            IRecurringJobManager recurringJob,
            IServiceProvider services,
            IBus publisher)
        {
            this.recurringJob = recurringJob;
            this.services = services;
            this.publisher = publisher;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            using (var scope = this.services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<DbContext>();

                if (!dbContext.Database.CanConnect())
                {
                    dbContext.Database.Migrate();
                }
            }

            this.recurringJob.AddOrUpdate(
                nameof(MessagesHostedService),
                () => this.ProcessPendingMessages(),
                "*/5 * * * * *");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        public void ProcessPendingMessages()
        {
            using var scope = this.services.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<DbContext>();

            var messages = dbContext
                .Set<Message>()
                .Where(m => !m.Published)
                .OrderBy(m => m.Id)
                .ToList();

            foreach (var message in messages)
            {
                this.publisher.Publish(message.Data, message.Type);

                message.MarkAsPublished();

                dbContext.SaveChanges();
            }
        }
    }
}
