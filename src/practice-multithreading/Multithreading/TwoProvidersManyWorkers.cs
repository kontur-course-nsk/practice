using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal sealed class TwoProvidersManyWorkers
    {
        private readonly UserJobProvider userJobProvider = new();
        private readonly AdminProvider adminProvider = new();
        private readonly Handler handler = new();

        public Task StartAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
