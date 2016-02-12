using System;
using System.Threading;

namespace CPTaskRunner
{
    public class DifferentProcessor : IProcessRunner
    {
        public string AnotherMessage { get; set; }

        public void Execute()
        {
            DoWork();
        }

        private void DoWork()
        {
            Thread.Sleep(2000);
            Console.WriteLine($"Task Executed with message: { AnotherMessage}");
        }
    }
}