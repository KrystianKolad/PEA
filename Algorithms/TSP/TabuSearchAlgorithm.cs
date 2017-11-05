using PEA.Algorithms.Interfaces;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;

namespace PEA.Algorithms.TSP
{
    public class TabuSearchAlgorithm : IAlgorithm
    {
        private readonly IReader _reader;
        private readonly ISymmetricalParser<City> _parser;

        public TabuSearchAlgorithm()
        {
            _reader = new TSPFileReader();
            _parser = new SymmetricalParser();
        }

        public void Run(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}