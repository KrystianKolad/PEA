using System;
using System.Collections.Generic;
using System.Linq;

namespace PEA.Model
{
    public class Population
    {
        private IList<Tuple<IList<int>,float>> _members;
        private Matrix _matrix;

        private int _membersCount;
        private int _crossCount;
        private int _mutationCount;

        private Random _random;

        public Population(Matrix matrix, int membersCount, int crossCount, int mutationCount)
        {
            _members = new List<Tuple<IList<int>,float>>();
            _matrix = matrix;
            _membersCount = membersCount;
            _crossCount = crossCount;
            _mutationCount = mutationCount;
            for (int i = 0; i < _membersCount; i++)
            {
                IList<int> cities = new List<int>();
                cities.Add(0);
                foreach(int item in CreateRandomMember())
                {
                    cities.Add(item);
                }
                cities.Add(0);
                float distance = _matrix.CalculateDistance(cities.Reverse().ToList());
                _members.Add(new Tuple<IList<int>, float>(cities,distance));
            }
        }

        public Tuple<IList<int>,float> GetBestMember(){

            return _members.OrderBy(x=>x.Item2).ToList()[0];
        }

        public void Cross()
        {
            for (int i = 0; i < _crossCount; i++)
            {
                _random = new Random();
                int firstMember = _random.Next(_membersCount/2);
                int secondMember = _random.Next(_membersCount/2,_membersCount);
                var firstSource = _members[firstMember].Item1;
                IList<int> cities = new List<int>(firstSource.Take(firstSource.Count/2));
                foreach(var item in _members[secondMember].Item1)
                {
                    if(!cities.Contains(item))
                    {
                        cities.Add(item);
                    }
                }
                cities.Add(0);
                var distance = _matrix.CalculateDistance(cities.Reverse().ToList());
                _members.Add(new Tuple<IList<int>, float>(cities,distance));
            }
        }

        public void Mutate()
        {
            for (int i = 0; i < _mutationCount; i++)
            {
                _random = new Random();
                int member = _random.Next(_membersCount);
                int firstCity = _random.Next(1,_matrix.Rows/2);
                int secondCity = _random.Next(_matrix.Rows/2,_matrix.Rows-1);
                var temp = _members[member].Item1[firstCity];
                _members[member].Item1[firstCity] = _members[member].Item1[secondCity];
                _members[member].Item1[secondCity] = temp;
            }
        }

        public void Iterate()
        {
            this.Cross();
            this.Mutate();
            this.Sort();
            this.Cut();
        }

        public void Sort()
        {
            _members = _members.OrderBy(x=>x.Item2).ToList();;
        }

        public void Cut()
        {
            _members =_members.Take(_membersCount).ToList();
        }

        public void Introduce(){
            foreach(var item in _members)
            {
                Console.WriteLine("Lista:");
                foreach(var city in item.Item1)
                {
                    Console.Write(city+"->");
                }
                Console.WriteLine("");
                Console.WriteLine("Odległość: " + item.Item2);
            }
        }

        private IList<int> CreateRandomMember () {
            IList<int> inputList = new List<int>();
            for (int i = 1; i < _matrix.Columns; i++)
            {
                inputList.Add(i);
            }
            IList<int> randomList = new List<int> ();

            Random r = new Random ();
            int randomIndex = 0;
            while (inputList.Count > 0) {
                randomIndex = r.Next (0, inputList.Count); //Choose a random object in the list
                randomList.Add (inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt (randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
    }
}