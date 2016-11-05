using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_lesson
{
	class Student
	{
		private string m_strName;	//字段，名字
		public int m_iAge;
		public double m_dScore;
		//private string m_strSex;
		private Gender m_sex;

		public string StrName		//属性
		{
			get
			{
				return m_strName;
			}

			set
			{
				if (value == "Dajie")
				{
					m_strName = "CHM";
					return;
				}
				m_strName = value;
			}
		}

		internal Gender Sex
		{
			get
			{
				return m_sex;
			}

			set
			{
				m_sex = value;
			}
		}

		public Student(string name, int age, double score, Gender sex)
		{
			StrName = name;
			m_iAge = age;
			m_dScore = score;
			Sex = sex;
		}

		public void Study()	//method = operate = function
		{
			Console.WriteLine("I'm {0}, I like studying", StrName);
		}

		public double GuessScore(double score)
		{
			if (score.Equals(m_dScore))
			{
				Console.WriteLine("You're right");
			} 
			else
			{
				Console.WriteLine("Wrong! {0}", m_dScore);
			}
			return m_dScore;
		}
	} 
}
