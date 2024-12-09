using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InveonBootcamp.App.SectionTwo
{
    public class AsyncProgrammingTasks
    {
        public static void ImplementLongTaskSync()
        {
            Console.WriteLine("Starting a long task synchronously.");
            Task.Delay(3000).Wait(); // Blocks the thread
            Console.WriteLine("Synchronous task finished.");
        }

        public static async Task ImplementLongTaskAsync()
        {
            Console.WriteLine("Starting a long task asynchronously.");
            await Task.Delay(3000); 
            Console.WriteLine("Asynchronous task finished.");
        }

        public static async Task ImplementTaskAllMethod()
        {
            var tasks = new Task[]
            {
                Task.Run(() => Console.WriteLine("Task 1 running.")),
                Task.Run(() => Console.WriteLine("Task 2 running.")),
                Task.Run(() => Console.WriteLine("Task 3 running."))
            };

            Console.WriteLine("Waiting for all tasks to complete.");
            await Task.WhenAll(tasks);
            Console.WriteLine("All tasks completed simultaneously.");
        }

        public static async Task PerformTaskWithErrorHandling()
        {
            try
            {
                Console.WriteLine("Starting task with error handling mechanism.");
                await Task.Run(() =>
                {
                    throw new InvalidOperationException("Something went wrong.");
                });
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid Operation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Error handling task completed.");
            }
        }

        public static async Task DemonstrateTaskWhenAny()
        {
            var tasks = new[]
            {
                SimulateTaskWithDelay("Download 1", 3000),
                SimulateTaskWithDelay("Download 2", 2000),
                SimulateTaskWithDelay("Download 3", 4000)
            };

            Console.WriteLine("Waiting for the first task to complete.");
            Task completedTask = await Task.WhenAny(tasks);
            Console.WriteLine("First task completed successfully.");
        }

        public static async Task DemonstrateTaskCancellation(CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Starting a long-running task.");
                await Task.Delay(5000, cancellationToken);
                Console.WriteLine("Task completed successfully.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task was cancelled before completion.");
            }
        }

        public static async Task PerformHttpRequestAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Initiating HTTP request.");
                    string result = await httpClient.GetStringAsync("https://api.example.com/data");
                    Console.WriteLine($"Received data: {result}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP request failed: {ex.Message}");
                }
            }
        }

        public static async Task<int> PerformComplexCalculationAsync()
        {
            Console.WriteLine("Starting complex calculation.");

            return await Task.Run(() =>
            {
                int result = 0;
                for (int i = 0; i < 1000000; i++)
                {
                    result += i;
                }
                return result;
            });
        }

        public static async Task PerformMultiStepAsyncOperationAsync()
        {
            try
            {
                Console.WriteLine("Starting multi-step async operation.");

                string preparedData = await PrepareDataAsync();
                string processedData = await ProcessDataAsync(preparedData);
                await SaveDataAsync(processedData);

                Console.WriteLine("Multi-step async operation completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in multi-step operation: {ex.Message}");
            }
        }

        private static async Task<string> PrepareDataAsync()
        {
            await Task.Delay(1000);
            return "Raw Data Prepared";
        }

        private static async Task<string> ProcessDataAsync(string data)
        {
            await Task.Delay(1500);
            return $"Processed: {data}";
        }

        private static async Task SaveDataAsync(string processedData)
        {
            await Task.Delay(1000);
            Console.WriteLine($"Data Saved: {processedData}");
        }

        private static async Task SimulateTaskWithDelay(string taskName, int delayTime)
        {
            Console.WriteLine($"{taskName} started.");
            await Task.Delay(delayTime);
            Console.WriteLine($"{taskName} completed after {delayTime}ms.");
        }

        public static async Task ProcessCollectionInParallelAsync(List<string> items)
        {
            var processingTasks = items.Select(async item =>
            {
                await Task.Delay(500);
                Console.WriteLine($"Processed item: {item}");
            });

            await Task.WhenAll(processingTasks);
            Console.WriteLine("All items processed in parallel.");
        }
    }
}