using System;
using System.Threading;
using System.Threading.Tasks;

namespace CPTaskRunner
{
    public class Processor
    {
        private Task runner;

        public Processor()
        {
            runner = new Task(() => { DoWork(); });
        }

        public async void Run()
        {
            runner.RunSynchronously();
        }

        private void DoWork()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Task Executed");
        }
    }
}