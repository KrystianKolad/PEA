using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PEA.Model;

namespace PEA.Algorithms.Abstract
{
    public abstract class AbstractTabuSearchAlgorithm
    {
        private TabuList _tabuList;
        private Matrix _matrix;
        private Stopwatch _watch;
        private int _numberOfIterations;
        private int _problemSize;
        private IList<int> _currentSolution;
        private IList<int> _bestSolution;
        private float _bestCost;
        public double Execute(Matrix matrix)
        {
            _watch = new Stopwatch();
            _matrix = matrix;
            _problemSize = _matrix.Rows;
            _numberOfIterations = _problemSize*10;
            _tabuList = new TabuList(_problemSize);
            SetupCurrentSolution();
            SetupBestSolution();
            _watch.Start();
            for (int i = 0; i < _numberOfIterations; i++)
            {
                int from = 0;
                int to = 0;

                for (int j = 1; j < _currentSolution.Count - 1; j++)
                {
                    for (int k = 2; k < _currentSolution.Count - 1; k++)
                    {
                        if(j!=k){
                            Swap(j,k);
                            float currentCost = _matrix.CalculateDistance(_currentSolution);
                            if((currentCost<_bestCost) && _tabuList.GetField(j,k)==0)
                            {
                                from = j;
                                to = k;
                                _bestSolution = new List<int>(_currentSolution);
                                _bestCost = currentCost;
                            }
                        }
                        
                    }
                }

                if(from!=0)
                {
                    _tabuList.Decrement();
                    _tabuList.Move(from, to);
                }
            }
            _watch.Stop();
            ShowResult(_bestSolution.Reverse().ToList());

            return _watch.Elapsed.TotalMilliseconds;
        }

        private void SetupCurrentSolution()
        {
            _currentSolution = new List<int>();
            for (int i = 0; i < _problemSize; i++)
            {
                _currentSolution.Add(i);
            }
            _currentSolution.Add(0);
        }

        private void SetupBestSolution()
        {
            _bestSolution = new List<int>(_currentSolution);
            _bestCost = _matrix.CalculateDistance(_bestSolution);
        }

        private void Swap(int x, int y)
        {
            int temp = _currentSolution[x];
            _currentSolution[x]=_currentSolution[y];
            _currentSolution[y] = temp;
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