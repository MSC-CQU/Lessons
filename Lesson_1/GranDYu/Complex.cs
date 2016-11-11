using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CComplex
{
	/// <summary>
	/// 复数类，封装了复数的加法、减法和乘法，打印复数的方法。
	/// 复数以a + bi的形式存储
	/// </summary>
	class Complex
	{
		/// <summary>
		/// 实部字段的属性定义
		/// </summary>
		public double Real
		{
			get
			{

				return m_dReal;
			}

			set
			{
				m_dReal = value;
			}
		}

		/// <summary>
		/// 虚部字段的属性定义
		/// </summary>
		public double Image
		{
			get
			{
				return m_dImage;
			}

			set
			{
				m_dImage = value;
			}
		}

		/// <summary>
		/// 无实参构造函数，实例化一个0
		/// </summary>
		public Complex()
		{
			Real = Image = 0;
		}

		/// <summary>
		/// 构造函数，实例化a + bi
		/// </summary>
		/// <param name="real">实部</param>
		/// <param name="image">虚部</param>
		public Complex(double real, double image)
		{
			Real = real;
			Image = image;
		}

		/// <summary>
		/// 按照“a + bi”的格式打印复数，在实部或虚部为0时打印一部分；在虚部为负数时改变连接处正负号
		/// </summary>
		public void PrintComplex()
		{
			if (Image.Equals(0.0))
			{
				if (Real.Equals(0.0))
				{
					//0 + 0i
					Console.WriteLine("{0}", 0);
				} 
				else
				{
					//a + 0i
					Console.WriteLine("{0}", Real);
				}
			} 
			else
			{
				if (Real.Equals(0.0))
				{
					//0 + bi
					Console.WriteLine("{0}i", Image);
				} 
				else
				{
					//a + bi
					int sure = Image.CompareTo(0.0).CompareTo(0);
					char flag = (sure > 0) ? '+' : '-';
					Console.WriteLine("{0} {1} {2}i", Real, flag, Math.Abs(m_dImage));
				}
			}
		}

		/// <summary>
		/// 加法，将this与right相加
		/// </summary>
		/// <param name="right">另一个加数</param>
		/// <returns></returns>
		public Complex Add(Complex right)
		{
			//(a + bi) + (c + di) = (a + c) + (b + d)i
			Complex result = new Complex(this.Real + right.Real, this.Image + right.Image);
			return result;
		}

		/// <summary>
		/// 减法，将this与right相减
		/// </summary>
		/// <param name="right">减数</param>
		/// <returns></returns>
		public Complex Sub(Complex right)
		{
			//(a + bi) - (c + di) = (a - c) + (b - d)i
			Complex result = new Complex(this.Real - right.Real, this.Image - right.Image);
			return result;
		}

		/// <summary>
		/// 乘法，将this与right相乘
		/// </summary>
		/// <param name="right">另一个乘数</param>
		/// <returns></returns>
		public Complex Mul(Complex right)
		{
			double res_real = 0, res_image = 0;
			//(a + bi)(c + di) = (ac - bd) + (ad + bc)i
			res_real = this.Real * right.Real - this.Image * right.Image;
			res_image = this.Real * right.Image + this.Image * right.Real;
			Complex result = new Complex(res_real, res_image);
			return result;
		}

		/// <summary>
		/// 复数的实部字段
		/// </summary>
		private double m_dReal;

		/// <summary>
		/// 复数的虚部字段
		/// </summary>
		private double m_dImage;
	}
}
