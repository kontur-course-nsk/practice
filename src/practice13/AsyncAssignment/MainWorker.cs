using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Async.External.Lib;

namespace AsyncAssignment
{
    public class MainWorker
    {
        private readonly AsyncWordCounter counter = new AsyncWordCounter();
        
        public string[] SplitToWordsAndCount(string input)
        {
            var words = AsyncWorker.SplitAsync(input).Result;
            counter.AddWordsAsync(words).Wait();
            return words;
        }

        public IEnumerable<KeyValuePair<string, int>> GetTop10АrequentlyUsedWords()
        {
            return counter.GetTop10АrequentlyUsedWords();
        }
    }
}
