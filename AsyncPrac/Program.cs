using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
/// <summary>
/// Practise set for Aync Programming
/// </summary>
namespace AsyncPrac
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread Starts...");

            //AyncTasks aTasks = new AyncTasks();
            //aTasks.ProgressReached += ATasks_ProgressReached;
            //Task.WaitAll(aTasks.DoSomethingAsync());

            WriteToFile w = new WriteToFile();
            byte[] b1 = Guid.NewGuid().ToByteArray();
            byte[] b2 = Guid.NewGuid().ToByteArray();
            byte[] b3 = Guid.NewGuid().ToByteArray();

            StreamWriter fs1 = new StreamWriter("file1.txt");
            StreamWriter fs2 = new StreamWriter("file2.txt");
            StreamWriter fs3 = new StreamWriter("file3.txt");
            int successIndex = Task.WaitAny(
                    w.WriteToFileAsync(fs1, b1, "FileStream1"),
                    w.WriteToFileAsync(fs2, b2, "FileStream2"),
                    w.WriteToFileAsync(fs3, b3, "FileStream3")
                );
            Console.WriteLine("Task: {0} has been completed", successIndex);



            Console.WriteLine("Main Thread Ends...");


        }

        private static void ATasks_ProgressReached(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Progress: {0}%", e.ProgressPercentage);
        }
    }

    class AyncTasks
    {
        public async Task DoSomethingAsync()
        {
            int value = 13;

            await Task.Delay(TimeSpan.FromSeconds(2));
            OnProgressReached(50);
            value *= 2;

            await Task.Delay(TimeSpan.FromSeconds(2));
            OnProgressReached(100);
            Console.WriteLine($"The output is {value}");
        }

        public event EventHandler<ProgressChangedEventArgs> ProgressReached;
        protected virtual void OnProgressReached(int currentProgress)
        {
            ProgressReached?.Invoke(this, new ProgressChangedEventArgs(currentProgress, null));
        }
    }

    class WriteToFile
    {
        public async Task<string> WriteToFileAsync(StreamWriter writer, byte[] bytes, string name)
        {
            Console.WriteLine("Initializing Task: {0} with Delay:", name);
            Random RandDelay = new Random();

            TimeSpan Delay = TimeSpan.FromMilliseconds(RandDelay.Next(30, 200));
            Console.WriteLine(Delay.Milliseconds + "ms");
            foreach (byte b in bytes)
            {
                await writer.WriteLineAsync(b.ToString("X2"));
                await Task.Delay(Delay);
                Console.WriteLine("{0} wrote: {1}"
                    , name
                    , b.ToString("X2"));
            }
            Console.WriteLine("{0} writting task completed.", name);
            writer.Close();
            return await Task.FromResult(writer.ToString());
        }
    }
}
