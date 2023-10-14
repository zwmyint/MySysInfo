using Hangfire;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Options;

namespace MyHangfire.ConsoleApp
{
    class Program
    {
        //
        static Dictionary<int, int> _jobsToRetriesMap = new Dictionary<int, int>
        {
            {1,0 },
            {5,0 },
            {8,0 },
            {10,0 },
            {11,0 },
            {12,0 },
            {15,0 },
            {20,0 },
            {25,0 }
        };

        static readonly int TRIES_TILL_SUCCESS = 3;

        static void Main(string[] args)
        {
            //JobStorage storage = new MemoryStorage.MemoryStorage(new MemoryStorageOptions());
            JobStorage storage = new MemoryStorage();

            LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());
            var serverOptions = new BackgroundJobServerOptions() { ShutdownTimeout = TimeSpan.FromSeconds(5) };

            using (var server = new BackgroundJobServer(serverOptions, storage))
            {
                Log("Hangfire Server started. Press any key to exit...", LogLevel.Fatal);

                JobStorage.Current = storage;

                BackgroundJob.Enqueue(() => Console.WriteLine("Easy!"));
                BackgroundJob.Enqueue(() => Log("Hello Hangfire!", LogLevel.Error));

                foreach (var job in _jobsToRetriesMap)
                {
                    Log($"Scheduling job ID :{job.Key} in {job.Key} seconds..", LogLevel.Warn);
                    BackgroundJob.Schedule(() => DoJob("Hi!", job.Key), TimeSpan.FromSeconds(job.Key));
                }
                System.Console.ReadKey();
                Log("Stopping server...", LogLevel.Fatal);
            }
        }

        //
        public static void DoJob(string jobMsg, int id)
        {
            Log($"Processing Job -> {jobMsg} : {id}  ..", LogLevel.Info);
            if (id % 2 == 0 && _jobsToRetriesMap[id] < TRIES_TILL_SUCCESS)
            {
                _jobsToRetriesMap[id] = _jobsToRetriesMap[id] + 1;
                Log($"JOB FAILED! -> {id} #{_jobsToRetriesMap[id]}", LogLevel.Fatal);
                //throw new ApplicationException("Delayed Task Failed..");
            }
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Log($"Job DONE! -> {jobMsg} : {id}  ..", LogLevel.Warn);
        }
        public static void Log(string msg, LogLevel level = LogLevel.Info)
            => LogProvider.GetLogger("Main").Log(logLevel: level, messageFunc: () => { return msg; });

        //
    }
}