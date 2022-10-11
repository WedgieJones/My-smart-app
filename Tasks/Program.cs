namespace Tasks;

class Program
{
	static async Task Main(string[] args)
	{
		Console.ReadKey();
		/*Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");

		for (int i = 0; i < 5000; i++)
		{

			Task.Run(() =>
			{
				Console.WriteLine($"#Task:{Task.CurrentId} \t Thread: {Thread.CurrentThread.ManagedThreadId}");
				Task.Delay(10000);
				Console.WriteLine($"#Task:{Task.CurrentId} \t Thread: {Thread.CurrentThread.ManagedThreadId} - completed ");

			});

		}*/

		await OrderAsync();

		Console.ReadKey();

	}

	private static async Task OrderAsync()
	{
		Console.Clear();
		await Task.Delay(4000);
		Console.WriteLine("Din beställning är skickad");

		await Task.Delay(2000);
		Console.WriteLine("Din beställning är mottagen");

		await Task.Delay(2000);
		Console.WriteLine("Din beställning tillags");
		
		await Task.Delay(5000);
		Console.WriteLine("Din beställning är klar för leverans");
		
		await Task.Delay(4000);
		Console.WriteLine("Din beställning är nu levererad");

	}


}
