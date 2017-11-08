using System.Collections.Generic;
using PEA.Algorithms.Interfaces;
using PEA.Factory.Interfaces;
using ATSP = PEA.Algorithms.ATSP;
using TSP = PEA.Algorithms.TSP;

namespace PEA.Factory
{
    public class AlgorithmFactory : IFactory
    {
        private Dictionary<int, IAlgorithm> _algorithms;
        public AlgorithmFactory()
        {
            _algorithms = new Dictionary<int, IAlgorithm>();
            _algorithms.Add(1, new TSP.DynamicAlgorithm());
            _algorithms.Add(2, new ATSP.DynamicAlgorithm());
            _algorithms.Add(3, new TSP.TabuSearchAlgorithm());
            _algorithms.Add(4, new ATSP.TabuSearchAlgorithm());
            _algorithms.Add(5, new TSP.GeneticAlgorithm());
            _algorithms.Add(6, new ATSP.GeneticAlgorithm());
        }
        public IAlgorithm GetAlgorithm(int number)
        {
            return _algorithms.GetValueOrDefault(number);
        }
    }
}