using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PEA.Model;

namespace PEA.Algorithms.Abstract {
    public abstract class AbstractTabuSearchAlgorithm {
        private TabuList _tabuList;
        private Matrix _matrix;
        private Stopwatch _watch;
        private int _numberOfIterations;
        private int _problemSize;
        private IList<int> _currentSolution;
        private IList<int> _bestSolution;
        private float _bestCost;
        public Tuple<float, double> Execute (Matrix matrix, int iterations, int cadency) {
            _watch = new Stopwatch ();
            _matrix = matrix;
            _problemSize = _matrix.Rows;
            _numberOfIterations = iterations;
            _tabuList = new TabuList (_problemSize);
            SetupCurrentSolution ();
            SetupBestSolution ();
            int l = 0;
            _watch.Start ();
            for (int i = 0; i < _numberOfIterations; i++) {
                int from = 0;
                int to = 0;
                float iterationBestCost = float.MaxValue;
                for (int j = 1; j < _currentSolution.Count - 1; j++) {
                    for (int k = 1; k < _currentSolution.Count - 1; k++) {
                        if (j != k && _tabuList.GetField (j, k) == 0) {
                            SwapCurrentSolution (j, k);
                            float currentCost = _matrix.CalculateDistance (_currentSolution);
                            if ((currentCost < iterationBestCost)) {
                                from = j;
                                to = k;
                                iterationBestCost = currentCost;
                                l = 0;
                            } else {
                                _currentSolution = new List<int> (_bestSolution);
                                l++;
                            }
                        }
                        if (l == 5) {
                            _currentSolution = this.ShuffleList (_currentSolution);
                            l = 0;
                        }
                    }
                }
                SwapBestSolution (from, to);
                _bestCost = _matrix.CalculateDistance (_bestSolution);
                if (from != 0) {
                    _tabuList.Decrement ();
                    _tabuList.Move (from, to, cadency);
                }
            }
            _watch.Stop ();
            ShowResult (_bestSolution.Reverse ().ToList ());

            return new Tuple<float, double> (_bestCost, _watch.Elapsed.TotalMilliseconds);
        }

        private void SetupCurrentSolution () {
            _currentSolution = new List<int> ();
            for (int i = 0; i < _problemSize; i++) {
                _currentSolution.Add (i);
            }
            _currentSolution.Add (0);
        }

        private void SetupBestSolution () {
            _bestSolution = new List<int> (_currentSolution);
            _bestCost = _matrix.CalculateDistance (_bestSolution);
        }

        private void SwapCurrentSolution (int j, int k) {
            int temp = _currentSolution[j];
            _currentSolution[j] = _currentSolution[k];
            _currentSolution[k] = temp;
        }

        private void SwapBestSolution (int j, int k) {
            int temp = _bestSolution[j];
            _bestSolution[j] = _bestSolution[k];
            _bestSolution[k] = temp;
        }
        private void ShowResult (IList<int> path) {
            Console.WriteLine ("Miasta:");
            for (int i = 0; i < path.Count; i++) {
                if (i == path.Count - 1) {
                    Console.WriteLine (path[i]);
                } else {
                    Console.Write (path[i] + "->");
                }
            }
            Console.WriteLine ("Odleglosc: " + _bestCost);
        }

        private IList<int> ShuffleList (IList<int> inputList) {
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