using PEA.Algorithms.Interfaces;
using PEA.Consts;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;

namespace PEA.Algorithms.ATSP
{
    public class DynamicAlgorithm : IAlgorithm
    {
        private IReader _reader;
        private IAsymmetricalParser<Matrix> _parser;
        public DynamicAlgorithm()
        {
            _reader = new TSPFileReader();
            _parser = new AsymmetricalParser();
        }

        public void Run(string fileName)
        {
            var data = _reader.Read(GlobalConsts.ATSPFilePath+fileName);
            var matrix = _parser.ParseData(data);
        }
    }
}