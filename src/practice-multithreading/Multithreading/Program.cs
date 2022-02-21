using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    public static class Program
    {
        public static async Task Main()
        {
            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            var oneProviderOneWorker = new OneProviderOneWorker();
            await oneProviderOneWorker.StartAsync(cts.Token);

            // var oneProviderOneWorkerWithErrors = new OneProviderOneWorkerWithErrors();
            // await oneProviderOneWorkerWithErrors.StartAsync(cts.Token);

            // var oneProviderManyWorkers = new OneProviderManyWorkers();
            // await oneProviderManyWorkers.StartAsync(cts.Token);

            // var twoProvidersManyWorkers = new TwoProvidersManyWorkers();
            // await twoProvidersManyWorkers.StartAsync(cts.Token);

            // var oneProviderManyWorkersWithLockByUserId = new OneProviderManyWorkersWithLockByUserId();
            // await oneProviderManyWorkersWithLockByUserId.StartAsync(cts.Token);
        }
    }
}
