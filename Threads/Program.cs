using System.Threading.Channels;

namespace Threads;

class Program
{
	static void Main(string[] args)
	{
		Console.ReadKey();
		Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");

		for (int i = 0; i < 5000; i++)
		{
			new Thread(() =>
			{
				Console.WriteLine($"{i} \t Thread: {Thread.CurrentThread.ManagedThreadId}");
				Thread.Sleep(1000);

			}).Start();
		}
		
		Console.ReadKey();


	}
}