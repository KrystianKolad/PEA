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
        public double Execute(Matrix matrix)
        {
            //matrix.Introduce();
            _watch = new Stopwatch();
            _matrix = matrix;
            _memory = new Dictionary<Tuple<int, IList<int>>, Tuple<int, float>>();
            int startCity = StartCity;
            int lastVisited = -1;
            IList<int> cities = new List<int>();
            IList<int> path = new List<int>();
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
                cities.Remove(lastVisited);
                Console.WriteLine("Mam miasto");
            }
            _watch.Stop();
            ShowResult(path.Reverse().ToList());
            return _watch.Elapsed.TotalMilliseconds;
        }

        private (int city, float distance) Rec(int currentCity, IList<int> cities)
        {
            var key = new Tuple<int, IList<int>>(currentCity,cities);
            if(_memory.ContainsKey(key))
            {
                var value = _memory[key];
                return (value.Item1,value.Item2);     
            }
            foreach (int city in cities)
            {
                if (currentCity == city)
                {
                    return (-1,-1);
                }
            }
            if (cities.Count > 0)
            {
                Dictionary<int, float> problems = new Dictionary<int, float>();
                foreach (var city in cities)
                {
                    var newCities = new List<int>(cities);
                    newCities.Remove(city);
                    problems.Add(city, _matrix.GetField(city, currentCity) + Rec(city, newCities).distance);
                }
                var min = problems.OrderBy(x => x.Value).First();
                var value = new Tuple<int, float>(min.Key,min.Value);
                _memory.Add(key,value);
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