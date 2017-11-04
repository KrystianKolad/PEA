using PEA.Model.Interfaces;

namespace PEA.Model
{
    public class Matrix : IEntity
    {
        private float?[,] _data;
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _data = new float?[Rows, Columns];
        }

        public void SetField(int row, int column, float? data)
        {
            _data[row, column] = data;
        }

        public float? GetField(int row, int column)
        {
            return _data[row, column];
        }

        public void Introduce()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    System.Console.Write(string.Format("{0, 10}",this.GetField(i,j)));
                }
                System.Console.WriteLine();
            }
        }
    }
}