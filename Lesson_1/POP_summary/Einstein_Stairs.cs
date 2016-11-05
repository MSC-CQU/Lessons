using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index_1_homework
{
	class Einstein_Stairs
	{
		public static void Calculate()
		{
			bool bFound = false;
			int i;
			for (i = 7; !bFound; i+=14)
			{
				bFound = (i % 3 == 2) && (i % 5 == 4) && (i % 6 == 5) && (i % 7 == 0);
				if (bFound)
				{
					break;
				}
			}
			//i--;//'cause when it was found, i is added by 1
			Console.WriteLine("the stairs are {0}", i);
		}
	}
}
