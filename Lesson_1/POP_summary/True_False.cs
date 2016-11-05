using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index_1_homework
{
	class True_False
	{
		public static void Judgement()
		{
			int sum = 0;
			bool flag = false;
			char killer;
			for (int i = 0; i < 4; i++)
			{
				killer = Convert.ToChar(64 + i);

				//when meet a "truth", sum++
				sum = Convert.ToInt32(killer != 'A') + Convert.ToInt32(killer == 'C') 
					+ Convert.ToInt32(killer == 'D') + Convert.ToInt32(killer != 'D');
				
				//check sum's value
				if (sum == 3)
				{
					Console.WriteLine("{0} is the killer.", killer);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Console.WriteLine("can not find the killer.");
			}
		}
	}
}
