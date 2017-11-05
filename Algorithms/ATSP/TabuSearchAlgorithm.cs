using PEA.Algorithms.Interfaces;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;

namespace PEA.Algorithms.ATSP
{
    public class TabuSearchAlgorithm : Abstract.AbstractTabuSearchAlgorithm, IAlgorithm
    {
        private IReader _reader;
        private IParser<Matrix> _parser;

        public TabuSearchAlgorithm()
        {
            _reader = new TSPFileReader();
            _parser = new AsymmetricalParser();
        }

        public void Run(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}