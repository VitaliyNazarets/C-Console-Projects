using System;

namespace BrownianMotion
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Brownian Motion");
			Console.WriteLine(new string('-', 25));

			Correct.ParseDouble("Enter the probability to change position to right (0-1)", out double probability, 0, 1);
			Correct.ParseInt("Enter the amount of reagent", out int reagentCount, 0);
			Correct.ParseInt("Enter the amount of cells", out int cellsCount, 1);
			Correct.ParseInt("Enter the number of repetitions", out int repetitionsCount, 1);
			Correct.ParseInt("Enter the number of output frequency", out int frequensy, 1, repetitionsCount);

			BrownianMotionSimulator brownianMotionSimulator = new BrownianMotionSimulator(probability, reagentCount, cellsCount);
			brownianMotionSimulator.Start(frequensy, repetitionsCount);

			Console.ReadKey();
		}
	}
}
