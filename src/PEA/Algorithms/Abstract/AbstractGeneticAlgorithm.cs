using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PEA.Model;

namespace PEA.Algorithms.Abstract
{
    public abstract class AbstractGeneticAlgorithm
    {
        private Population _population;
        private Matrix _matrix;
        Stopwatch _watch;
        public Tuple<float, double>  Execute(Matrix matrix, int iterations, int membersCount, int crossCount, int mutationCount)
        {
            _matrix = matrix;
            _watch = new Stopwatch();
            _population = new Population(_matrix,membersCount,crossCount,mutationCount);
            _watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                _population.Iterate();
            }
            _watch.Stop();
            var best = _population.GetBestMember();
            this.ShowResult(best.Item1.Reverse().ToList());
            return new Tuple<float, double> (best.Item2, _watch.Elapsed.TotalMilliseconds);
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