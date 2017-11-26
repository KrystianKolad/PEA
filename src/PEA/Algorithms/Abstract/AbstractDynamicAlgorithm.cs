using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using PEA.Model;

namespace PEA.Algorithms.Abstract
{
    public abstract class AbstractDynamicAlgorithm
    {
        //miasto początkowe
        private const int StartCity = 0;
        private Matrix _matrix;
        // miejsce do zapisywania odległosci dla pary (miasto, lista miast do przejscia)
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
            //miasta do odwiedzenia
            IList<int> cities = new List<int>();
            //droga, ktora jest rezultatem algorytmu
            IList<int> path = new List<int>();
            path.Add(StartCity);
            //Dodajemy pozostałe miasta do miast do odwiedzenia
            for (int i = 1; i < matrix.Rows; i++)
            {
                cities.Add(i);
            }
            _watch.Start();
            while (lastVisited != StartCity)
            {
                //wywolujemy funkcję rekurencyjną, która szuka najbardziej optymalnego miasta
                lastVisited = Rec(startCity, cities).city;
                //dodajemy znalezione miasto do optymalnej drogi
                path.Add(lastVisited);
                //ustawiamy miasto startowe na obecne
                startCity = lastVisited;
                //usuwamy juz odwiedzone miasto z listy miast do odwiedzenia
                cities.Remove(lastVisited);
            }
            _watch.Stop();
            //ShowMemory();
            ShowResult(path.Reverse().ToList());
            return _watch.Elapsed.TotalMilliseconds;
        }

        private (int city, float distance) Rec(int currentCity, IList<int> cities)
        {
            //sprawdzamy, czy podane miasto oraz lista miast istnieją w pamięci
            //jesli tak, zwracamy wartosc
            var key = new Tuple<int, IList<int>>(currentCity,cities);
            if(_memory.ContainsKey(key))
            {
                var value = _memory[key];
                return (value.Item1,value.Item2);     
            }
            //sprawdzamy, czy miasto znajduje się na liscie miast do przejscia
            foreach (int city in cities)
            {
                if (currentCity.Equals(city))
                {
                    return (-1,-1);
                }
            }
            //sprawdzamy, czy lista miast jest pusta
            if (cities.Count > 0)
            {
                //tworzymy liste podproblemow
                Dictionary<int, float> problems = new Dictionary<int, float>();
                //Dla kazdego miasta tworzymy podproblemy
                foreach (var city in cities)
                {
                    //tworzymy nową listę miast do przejscia dla danego podproblemu
                    var newCities = new List<int>(cities);
                    //usuwamy miasto z listy miast
                    newCities.Remove(city);
                    //znajdujemy podproblem dla kazdego miasta
                    problems.Add(city, _matrix.GetField(city, currentCity) + Rec(city, newCities).distance);
                }
                //szukamy miasta o najkrotszej drodze
                var min = problems.OrderBy(x => x.Value).First();
                var value = new Tuple<int, float>(min.Key,min.Value);
                //dodajemy nasz wynik do pamięci
                _memory.Add(key,value);
                //zwracamy miasto oraz drogę do niego
                return (min.Key, min.Value);
            }
            else
            {
                //jesli lista miast jest pusta zwracamy sciezke do miasta poczatkowego
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

        private void ShowMemory()
        {
            foreach (var pair in _memory)
            {
                Console.Write("Miasto: " + pair.Key.Item1 + ", Lista miast: {");
                foreach (var item in pair.Key.Item2)
                {
                    Console.Write(item+" ");
                }
                Console.WriteLine("}, Następne miasto: "+pair.Value.Item1+" odległosc: "+pair.Value.Item2);
            }
        }
    }
}