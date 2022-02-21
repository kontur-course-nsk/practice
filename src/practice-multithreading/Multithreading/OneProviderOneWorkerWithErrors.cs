using System;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading
{
    internal sealed class OneProviderOneWorkerWithErrors
    {
        private readonly UserJobProvider userJobProvider = new();
        private readonly Handler handler = new(true);

        public Task StartAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
