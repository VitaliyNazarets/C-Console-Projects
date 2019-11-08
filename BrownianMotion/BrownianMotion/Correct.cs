using System;

namespace BrownianMotion
{
	public static class Correct
	{
		public static void ParseInt(string MainText, out int result, int Min = int.MinValue, int Max = int.MaxValue)
		{
			Console.WriteLine(MainText);
			while (!int.TryParse(Console.ReadLine(), out result) || result < Min || result > Max) ;

		}
		public static void ParseDouble(string MainText, out double result, double Min = double.MinValue, double Max = double.MaxValue)
		{
			Console.WriteLine(MainText);
			while (!double.TryParse(Console.ReadLine(), out result) || result < Min || result > Max) ;

		}
	}
}
