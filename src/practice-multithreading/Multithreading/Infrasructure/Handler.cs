using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal sealed class Handler
    {
        private readonly bool withErrors;
        private readonly Random random = new();

        public Handler(bool withErrors = false)
        {
            this.withErrors = withErrors;
        }

        public async Task HandleUserJobAsync(UserJob userJob, CancellationToken token)
        {
            if (this.withErrors && this.random.Next(5) == 0)
            {
                throw new Exception("ERROR");
            }

            await Task.Delay(this.random.Next(1000, 3000));
            lock (this)
            {
                Console.WriteLine($"User job with id {userJob.JobId} and user id {userJob.UserId} handled.");
            }
        }

        public async Task HandleAdminJobAsync(AdminJob organizationJob, CancellationToken token)
        {
            await Task.Delay(this.random.Next(1000, 3000));
            lock (this)
            {
                Console.WriteLine($"Admin job with id {organizationJob.JobId} handled.");
            }
        }
    }
}
