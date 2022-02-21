using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal sealed class AdminProvider
    {
        private readonly ConcurrentQueue<AdminJob> queue = new();
        private readonly Random random = new();

        public async Task StartAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(this.random.Next(200, 500));
                this.queue.Enqueue(new AdminJob { JobId = Guid.NewGuid() });
            }
        }

        public async Task<AdminJob> GetAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (this.queue.TryDequeue(out var job))
                {
                    return job;
                }

                await Task.Delay(100);
            }

            return null;
        }
    }
}
