using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BrownianMotion
{
	public class BrownianMotionSimulator
	{
		private double _probability { get; set; }
		private int _reagentCount { get; set; }
		private int _cellsCount { get; set; }
		static readonly object _locker = new object();

		private int[] arrayReagents
		{
			get;
			set;
		}
		public double Probability { get { return this._probability; } }
		public int ReagentCount { get { return this._reagentCount; } }
		public int CellsCount { get { return this._cellsCount; } }

		public int ReagentsCountNow()
		{
			int sum = 0;
			foreach (var countInCell in arrayReagents)
				sum += countInCell;
			return sum;
		}

		public void PrintCurrent()
		{
			Console.WriteLine(string.Join(", ", arrayReagents) + " Count now: " + ReagentsCountNow().ToString());
		}

		public Task[] Iteration()
		{
			Task[] tasks = new Task[CellsCount];
			for (int i = 0; i < tasks.Length; i++)
			{
				int tempId = i;
				tasks[tempId] = Task.Factory.StartNew(() => ReactionInCell(tempId));
			}
			return tasks;
		}

		int CountMoveLeft(int elementsCount)
		{
			Random r = new Random();
			int result = 0;
			for (int i = 0; i < elementsCount; i++)
				result = r.NextDouble() > Probability ? result + 1 : result;
			return result;
		}

		void ReactionInCell(int id)
		{
			lock (_locker)
			{
				int moveLeft = CountMoveLeft(arrayReagents[id]);
				int moveRight = arrayReagents[id] - moveLeft;
				if (id > 0)
				{
					arrayReagents[id] -= moveLeft;
					arrayReagents[id - 1] += moveLeft;
				}
				if (id < arrayReagents.Length - 1)
				{
					arrayReagents[id] -= moveRight;
					arrayReagents[id + 1] += moveRight;
				}
			}
		}

		public void Start(int printFrequensy, int repetitionsCount)
		{
			Task[] tasks = null;
			for (int i = 0; i < repetitionsCount; i++)
			{
				tasks = Iteration();
				if (i % printFrequensy == 0)
				{
					Task.WaitAll(tasks);
					PrintCurrent();
				}
			}
			Task.WaitAll(tasks);
			Console.WriteLine(new string('-', 25));
			PrintCurrent();
		}



		public BrownianMotionSimulator(double probability, int reagentCount, int cellsCount)
		{
			_probability = probability;
			_reagentCount = reagentCount;
			_cellsCount = cellsCount;
			arrayReagents = new int[_cellsCount];
			arrayReagents[0] = _reagentCount;
		}
	}
}
