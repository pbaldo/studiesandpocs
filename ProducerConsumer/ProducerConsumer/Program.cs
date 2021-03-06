﻿using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;
using CPTaskRunner;

namespace ProducerConsumer
{
    class Program
    {
        private static BackgroundWorker procducerWorker;
        private static BackgroundWorker consumerWorker;

        private static BlockingCollection<IProcessRunner> taskCollection;

        static void Main(string[] args)
        {
            taskCollection = new BlockingCollection<IProcessRunner>(25);
            procducerWorker = new BackgroundWorker();
            consumerWorker = new BackgroundWorker();
            procducerWorker.DoWork += ProcducerWorker_DoWork;
            consumerWorker.DoWork += ConsumerWorker_DoWork;
            procducerWorker.RunWorkerAsync();
            consumerWorker.RunWorkerAsync();

            while (true)
            { Thread.Sleep(1000); }
        }

        private static void ConsumerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var process = taskCollection.Take();
                Console.WriteLine("Removed thread from Queue");
                process.Execute();
            }
        }

        private static void ProcducerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                taskCollection.Add(new Processor() { Message = "Polymorphic Test, this is a Processor Object" });
                Console.WriteLine("Added thread to Queue");
                Console.WriteLine($"Number of threads in Queue: {taskCollection.Count}");

                taskCollection.Add(new DifferentProcessor() { AnotherMessage = "Polymorphic Test, this is a Different Processor Object" });
                Console.WriteLine("Added thread to Queue");
                Console.WriteLine($"Number of threads in Queue: {taskCollection.Count}");
            }
        }
    }
}