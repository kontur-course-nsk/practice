using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal sealed class UserJobProvider
    {
        private readonly ConcurrentQueue<UserJob> queue = new();
        private readonly Random random = new();
        private readonly int[] userIds = { 1, 2, 3 };

        public async Task StartAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(this.random.Next(100, 300));
                var userJob = new UserJob
                {
                    JobId = Guid.NewGuid(),
                    UserId = this.userIds[this.random.Next(this.userIds.Length)],
                };
                this.queue.Enqueue(userJob);
            }
        }

        public async Task<UserJob> GetAsync(CancellationToken token)
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
