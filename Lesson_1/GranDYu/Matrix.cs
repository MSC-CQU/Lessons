using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMatrix
{
	/// <summary>
	/// 矩阵类，封装了加法、乘法、求行列式值、求秩的方法
	/// </summary>
	class Matrix
	{
		/// <summary>
		/// 矩阵的行的属性
		/// </summary>
		public int Row
		{
			get
			{
				return m_iRow;
			}
		}

		/// <summary>
		/// 矩阵的列的属性
		/// </summary>
		public int Column
		{
			get
			{
				return m_iColumn;
			}
		}

		/// <summary>
		/// 构造一个矩阵，按 row * column 的规格填充0
		/// </summary>
		/// <param name="row">矩阵的行数</param>
		/// <param name="column">矩阵的列数</param>
		public Matrix(int row, int column)
		{
			m_iRow = row;
			m_iColumn = column;
			m_dData = new double[row][];
			for (int currentRow = 0; currentRow < row; currentRow++)
			{
				m_dData[currentRow] = new double[column];
				for (int currentCol = 0; currentCol < column; currentCol++)
				{
					m_dData[currentRow][currentCol] = 0.0;
				}
			}
		}

		/// <summary>
		/// 用一个二维数组构造一个矩阵
		/// </summary>
		/// <param name="table">用于填充矩阵的二维数组</param>
		public Matrix(double[][] table)
		{
			if (!CheckTable(table))
			{
				Console.WriteLine("error:can not create a matrix with the table");
				return;
			}
			this.m_iRow = table.Length;
			this.m_iColumn = table[0].Length;
			m_dData = new double[table.Length][];
			for (int row = 0; row < m_dData.Length; row++)
			{
				m_dData[row] = new double[table[row].Length];
			}
			for (int row = 0; row < m_iRow; row++)
			{
				for (int col = 0; col < m_iColumn; col++)
				{
					m_dData[row][col] = table[row][col];
				}
			}
		}

		/// <summary>
		/// 重新填充矩阵，从控制台获得输入
		/// </summary>
		public void Initialize()
		{
			for (int row = 0; row < Row; row++)
			{
				for (int col = 0; col < Column; col++)
				{
					Console.Write("input the ({0}, {1}) ->", row + 1, col + 1);
					this.m_dData[row][col] = Convert.ToDouble(Console.ReadLine());
				}
			}
		}

		/// <summary>
		/// 矩阵的加法
		/// </summary>
		/// <param name="B">右加的矩阵</param>
		/// <returns>和的矩阵</returns>
		public Matrix Add(Matrix B)
		{
			if (!this.Row.Equals(B.Row) || !this.Column.Equals(B.Column))
			{
				Console.WriteLine("error:A and B must have the same size");
				Matrix bad = new Matrix(this.Row, this.Column);
				return bad;
			}
			double[][] table = new double[B.Row][];
			for (int row = 0; row < m_iRow; row++)
			{
				table[row] = new double[B.Column];
				for (int col = 0; col < m_iColumn; col++)
				{
					table[row][col] = this.m_dData[row][col] + B.m_dData[row][col];
				}
			}
			Matrix result = new Matrix(table);
			return result;
		}

		/// <summary>
		/// 矩阵的乘法
		/// </summary>
		/// <param name="B">右乘的矩阵</param>
		/// <returns>积的矩阵</returns>
		public Matrix Mul(Matrix B)
		{
			if (!this.Column.Equals(B.Row))
			{
				Console.WriteLine("error:A's column must be equal to B's row");
				Matrix bad = new Matrix(this.Row, this.Column);
				return bad;
			}
			Matrix result = new Matrix(this.Row, B.Column);
			for (int currentResultCol = 0; currentResultCol < B.Column; currentResultCol++)
			{
				for (int currentResultRow = 0; currentResultRow < this.Row; currentResultRow++)
				{
					for (int currentMulCol = 0; currentMulCol < this.Column; currentMulCol++)
					{
						result.m_dData[currentResultRow][currentResultCol] += this.m_dData[currentResultRow][currentMulCol] * B.m_dData[currentMulCol][currentResultCol];
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 打印输出矩阵
		/// </summary>
		public void PrintMatrix()
		{
			for (int row = 0; row < Row; row++)
			{
				for (int col = 0; col < Column; col++)
				{
					Console.Write("{0,5}", m_dData[row][col]);
				}
				Console.WriteLine();
			}
		}

		/// <summary>
		/// 求矩阵的行列式的值
		/// </summary>
		/// <returns>行列式的结果，若不为方阵则返回0</returns>
		public double Determinant()
		{
			if (!this.Row.Equals(this.Column))
			{
				Console.WriteLine("error:Square Matrix has determinant");
				return 0;
			}
			Determinant det = new Determinant(this.m_dData);
			return det.CalculateDet();
		}

		/// <summary>
		/// 求矩阵的秩
		/// </summary>
		/// <returns>矩阵的秩</returns>
		public int Rank()
		{
			double[][] temp = new double[Row][];
			for (int row = 0; row < Row; row++)
			{
				temp[row] = new double[Column];
				for (int col = 0; col < Column; col++)
				{
					temp[row][col] = m_dData[row][col];
				}
			}
			SortNonzeroRow(temp);
			while (!CheckComplete(temp))
			{
				RowOperation(temp);
				SortNonzeroRow(temp);
			}
			for (int row = 0; row < Row; row++)
			{
				for (int col = 0; col < Column; col++)
				{
					if (Math.Abs(temp[row][col]) <= 1e-6)
					{
						temp[row][col] = 0;
					}
				}
			}
			return GetRank(temp);
		}

		/// <summary>
		/// 检查用于填充矩阵的二维数组的合法性
		/// </summary>
		/// <param name="table">待检查的二维数组</param>
		/// <returns>若合法，返回真；否则，返回假</returns>
		private bool CheckTable(double[][] table)
		{
			if (table.Length.Equals(0) || table[0].Length.Equals(0))
			{
				Console.WriteLine("unable to initialize a matrix with null");
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
			return true;
		}

		/// <summary>
		/// 矩阵是否变换到最简形式
		/// </summary>
		/// <param name="matrix">填充矩阵的二维数组</param>
		/// <returns>若为行最简则返回真；否则返回假</returns>
		private bool CheckComplete(double[][] table)
		{
			int[] count = new int[Row];
			for (int row = 0; row < Row; row++)
			{
				for (int col = 0; col < Column; col++)
				{
					if (table[row][col].Equals(0))
					{
						count[row]++;
					}
					else
					{
						break;
					}
				}
			}
			for (int row = 1; row < count.Length; row++)
			{
				if (count[row] <= count[row - 1] && count[row] != Column)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 按每行首个非零元素位置的先后排序所有的行
		/// </summary>
		/// <param name="matrix">填充矩阵的二维数组</param>
		private void SortNonzeroRow(double[][] matrix)
		{
			int[] counter = new int[Row];
			for (int i = 0; i < Row; i++)
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					if (matrix[i][j] == 0)
					{
						counter[i]++;
					}
					else
					{
						break;
					}
				}
			}
			for (int i = 0; i < counter.Length; i++)
			{
				for (int j = i; j < counter.Length; j++)
				{
					if (counter[i] > counter[j])
					{
						double[] dTemp = matrix[i];
						matrix[i] = matrix[j];
						matrix[j] = dTemp;
					}
				}
			}
		}

		/// <summary>
		/// 行初等变换
		/// </summary>
		/// <param name="matrix">用于填充矩阵的二维数组</param>
		private void RowOperation(double[][] matrix)
		{
			int[] counter = new int[Row];
			for (int row = 0; row < Row; row++)
			{
				for (int col = 0; col < Column; col++)
				{
					if (matrix[row][col].Equals(0))
					{
						counter[row]++;
					}
					else
					{
						break;
					}
				}
			}
			for (int i = 1; i < counter.Length; i++)
			{
				if (counter[i] == counter[i - 1] && counter[i] != Column)
				{
					double a = matrix[i - 1][counter[i - 1]];
					double b = matrix[i][counter[i]];
					matrix[i][counter[i]] = 0;
					for (int j = counter[i] + 1; j < Column; j++)
					{
						double c = matrix[i - 1][j];
						matrix[i][j] -= (c * b / a);
					}
					break;
				}
			}
		}

		/// <summary>
		/// 计算行最简矩阵的秩
		/// </summary>
		/// <param name="matrix">用于填充矩阵的二维数组</param>
		/// <returns>矩阵的秩</returns>
		private int GetRank(double[][] matrix)
		{
			int rank = -1;
			bool bIsAllZero = true;
			for (int row = 0; row < Row; row++)
			{
				bIsAllZero = true;
				for (int col = 0; col < Column; col++)
				{
					if (!matrix[row][col].Equals(0))
					{
						bIsAllZero = false;
						break;
					}
				}
				if (bIsAllZero)
				{
					return row;
				}
			}
			if (rank.Equals(-1))
			{
				rank = (Row < Column) ? Row : Column;
			}
			return rank;
		}

		/// <summary>
		/// 矩阵的行
		/// </summary>
		private int m_iRow;

		/// <summary>
		/// 矩阵的列
		/// </summary>
		private int m_iColumn;

		/// <summary>
		/// 储存矩阵内部数据
		/// </summary>
		private double[][] m_dData;
	}
}