using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMatrix
{
	/// <summary>
	/// 行列式类，封装了计算行列式值的方法
	/// </summary>
	class Determinant
	{
		/// <summary>
		/// 行列式的阶的字段的属性定义
		/// </summary>
		public int Order
		{
			get
			{
				return m_iOrder;
			}
		}

		/// <summary>
		/// 行列式的构造函数
		/// </summary>
		/// <param name="table">一个二维数组，用于填充行列式</param>
		public Determinant(double[][] table)
		{
			if (!CheckTable(table))
			{
				Console.WriteLine("error:can not initialize a determinant with the table");
				return;
			}
			this.m_iOrder = table.Length;
			m_dSquare = new double[Order][];
			for (int row = 0; row < Order; row++)
			{
				m_dSquare[row] = new double[Order];
				for (int column = 0; column < Order; column++)
				{
					m_dSquare[row][column] = table[row][column];
				}
			}
		}

		/// <summary>
		/// 取（i+1，j+1）的余子式
		/// </summary>
		/// <param name="i">行标减1</param>
		/// <param name="j">列标减1</param>
		/// <returns>返回该处的余子式（以行列式的形式返回）</returns>
		private Determinant GetMinorMij(int i, int j)
		{
			double[][] table = new double[this.Order - 1][];
			for (int Mrow = 0, Orow = 0; Orow < Order; Orow++)
			{
				table[Mrow] = new double[this.Order - 1];
				if (Orow == i)
				{
					continue;
				}
				for (int Mcol = 0, Ocol = 0; Ocol < Order; Ocol++)
				{

					if (Ocol == j)
					{
						continue;
					}
					else
					{
						table[Mrow][Mcol] = this.m_dSquare[Orow][Ocol];
						Mcol++;
					}
				}
				Mrow++;
			}
			Determinant det = new Determinant(table);
			return det;
		}

		/// <summary>
		/// 计算行列式的值
		/// </summary>
		/// <returns>返回该行列式的值（以浮点数的形式返回）</returns>
		public double CalculateDet()
		{
			double sum = 0;
			switch (this.Order)
			{
				case 0:
					Console.WriteLine("error:a determinant's order can not be zero");
					break;
				case 1:
					sum = m_dSquare[0][0];
					break;
				case 2:
					sum = m_dSquare[0][0] * m_dSquare[1][1] - m_dSquare[0][1] * m_dSquare[1][0];
					break;
				case 3:
					sum = m_dSquare[0][0] * m_dSquare[1][1] * m_dSquare[2][2] + m_dSquare[0][1] * m_dSquare[1][2] * m_dSquare[2][0] + m_dSquare[0][2] * m_dSquare[1][0] * m_dSquare[2][1] - m_dSquare[0][2] * m_dSquare[1][1] * m_dSquare[2][0] - m_dSquare[0][1] * m_dSquare[1][0] * m_dSquare[2][2] - m_dSquare[0][0] * m_dSquare[1][2] * m_dSquare[2][1];
					break;
				default:
					for (int i = 0; i < Order; i++)
					{
						sum += Math.Pow(-1 * 1.0, 1 + i + 1) * m_dSquare[0][i] * this.GetMinorMij(0, i).CalculateDet();
					}
					break;
			}
			return sum;
		}

		/// <summary>
		/// 检查用于填充行列式的二组数组的合法性
		/// </summary>
		/// <param name="table">待检查的二维数组</param>
		/// <returns>若合法，返回真；否则，返回假</returns>
		private bool CheckTable(double[][] table)
		{
			if (table.Length.Equals(0) || table[0].Length.Equals(0))
			{
				Console.WriteLine("unable to initialize a determinant with null");
				return false;
			}
			int col = table[0].Length;
			for (int row = 1; row < table.Length; row++)
			{
				if (!col.Equals(table[row].Length))
				{
					Console.WriteLine("each row must have the same elements");
					return false;
				}
			}
			if (!col.Equals(table.Length))
			{
				Console.WriteLine("Determinant's row must be equal to column");
				return false;
			}
			return true;
		}

		/// <summary>
		/// 行列式的阶的字段
		/// </summary>
		private int m_iOrder;

		/// <summary>
		/// 储存行列式的内部数据
		/// </summary>
		private double[][] m_dSquare;
	}
}