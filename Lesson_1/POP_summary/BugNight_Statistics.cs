using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index_1_homework
{
	class BugNight_Statistics
	{
		public static void Statistics()
		{
			Console.Write("input the size of the array->");
			int size = Convert.ToInt32(Console.ReadLine());

			//store 1 2 3's appearance
			int[] summary = new int[3] { 0, 0, 0 };

			for (int i = 0; i < size; i++)
			{
				Console.Write("input 1、2、3 ->");
				int input = Convert.ToInt32(Console.ReadLine());
				switch (input)
				{
					case 1:
						summary[0]++;
						break;
					case 2:
						summary[1]++;
						break;
					case 3:
						summary[2]++;
						break;
					default:
						//illegal, so give another chance
						Console.WriteLine("warning: input 1 or 2 or 3");
						i--;
						break;
				}
			}

			for (int i = 0; i < summary[0]; i++)
			{
				Console.WriteLine("1");
			}
			for (int i = 0; i < summary[1]; i++)
			{
				Console.WriteLine("2");
			}
			for (int i = 0; i < summary[2]; i++)
			{
				Console.WriteLine("3");
			}
		}
	}
}
