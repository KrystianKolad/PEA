using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using PEA.Model;

namespace PEA.Algorithms.Abstract
{
    public abstract class AbstractDynamicAlgorithm
    {
        private const int StartCity = 0;
        private Matrix _matrix;
        private Dictionary<Tuple<int,IList<int>>,Tuple<int,float>> _memory;
        Stopwatch _watch;
        public long Execute(Matrix matrix)
        {
            //matrix.Introduce();
            _watch = new Stopwatch();
            _matrix = matrix;
            _memory = new Dictionary<Tuple<int, IList<int>>, Tuple<int, float>>();
            int startCity = StartCity;
            int lastVisited = -1;
            IList<int> cities = new List<int>();
            IList<int> path = new List<int>();
            path.Add(startCity);
            for (int i = 1; i < matrix.Rows; i++)
            {
                cities.Add(i);
            }
            _watch.Start();
            while (lastVisited != StartCity)
            {
                lastVisited = Rec(startCity, cities).city;
                path.Add(lastVisited);
                startCity = lastVisited;
                cities.Remove(startCity);
                Console.WriteLine("Mam miasto");
            }
            _watch.Stop();
            ShowResult(path.Reverse().ToList());
            return _watch.ElapsedMilliseconds;
        }

        private (int city, float distance) Rec(int currentCity, IList<int> cities)
        {
            try
            {
                var memoryCheck = _memory[new Tuple<int, IList<int>>(currentCity,cities)];
                return (memoryCheck.Item1,memoryCheck.Item2);
            }
            catch (Exception)
            {
            }
            Dictionary<int, float> problems = new Dictionary<int, float>();
            if (cities.Count != 0)
            {
                foreach (var item in cities)
                {
                    var newCities = new List<int>(cities);
                    newCities.Remove(item);
                    problems.Add(item, _matrix.GetField(item, currentCity) + Rec(item, newCities).distance);
                }
                var min = problems.OrderBy(x => x.Value).First();
                var key = new Tuple<int, IList<int>>(currentCity,cities);
                var value = new Tuple<int, float>(min.Key,min.Value);
                if(_memory.ContainsKey(key))
                {
                    if(_memory[key].Item2>value.Item2)
                    {
                        _memory[key]=value;
                    }
                }
                else
                {
                    _memory.Add(key,value);
                }
                return (min.Key, min.Value);
            }
            else
            {
                return (StartCity, _matrix.GetField(StartCity, currentCity));
            }
        }

        private void ShowResult(IList<int> path)
        {
            float distance = 0;
            Console.WriteLine("Miasta:");
            for (int i = 0; i < path.Count; i++)
            {
                if (i == path.Count - 1)
                {
                    Console.WriteLine(path[i]);
                }
                else
                {
                    Console.Write(path[i] + "->");
                    distance += _matrix.GetField(path[i], path[i + 1]);
                }
            }
            Console.WriteLine("Odleglosc: " + distance);
        }
    }
}