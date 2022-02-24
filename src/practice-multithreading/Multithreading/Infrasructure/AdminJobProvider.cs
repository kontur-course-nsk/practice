using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal sealed class AdminJobProvider
    {
        private readonly ConcurrentQueue<AdminJob> queue = new();
        private readonly Random random = new();

        private bool started;

        public async Task StartAsync(CancellationToken token)
        {
            if (this.started)
            {
                throw new AlreadyStartedException(nameof(UserJobProvider));
            }

            this.started = true;

            while (!token.IsCancellationRequested)
            {
                await Task.Delay(this.random.Next(200, 500));
                this.queue.Enqueue(new AdminJob { JobId = Guid.NewGuid() });
            }
        }

        public async Task<AdminJob> GetAsync(CancellationToken token)
        {
            if (!this.started)
            {
                throw new NotStartedException(nameof(UserJobProvider));
            }

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
