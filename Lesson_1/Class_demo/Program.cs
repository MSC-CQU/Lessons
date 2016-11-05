using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lesson
{
	class Program
	{
		static void Main(string[] args)
		{
			Student Chairman = new Student("Dage", 3, 59.9, (Gender)11);

			Chairman.Sex = Gender.男;
			Chairman.StrName = "Dajie";

			Console.WriteLine("{0}'s sex is {1}, age is {2}, got {3}", Chairman.StrName, Chairman.Sex, Chairman.m_iAge, Chairman.m_dScore);

			Chairman.Study();

			double chmScore = 60;
			double truth = Chairman.GuessScore(chmScore);
			Console.WriteLine("chm's score is {0}", truth);

		}
	}
}
