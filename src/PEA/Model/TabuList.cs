using PEA.Model.Interfaces;

namespace PEA.Model
{
    public class TabuList : IEntity
    {
        private float[,] _data;
        private int _citiesNumber;

        public TabuList(int citiesNumber)
        {
            _citiesNumber = citiesNumber;
            _data = new float[_citiesNumber,_citiesNumber];
        }

        public void Move(int from, int to, int cadency)
        {
            _data[from,to] +=cadency;
            _data[to,from] +=cadency;
        }

        public float GetField(int row, int column)
        {
            return _data[row,column];
        }

        public void Decrement()
        {
            for(int i = 0; i<_citiesNumber; i++)
            {
                for(int j = 0; j<_citiesNumber; j++)
                {
                    _data[i,j]-=_data[i,j]<=0?0:1;
                }
            }
        }
    }
}