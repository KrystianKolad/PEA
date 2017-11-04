using PEA.Algorithms.Interfaces;
using PEA.Consts;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;

namespace PEA.Algorithms.TSP
{
    public class DynamicAlgorithm : IAlgorithm
    {
        private IReader _reader;
        private ISymmetricalParser<City> _parser;

        public DynamicAlgorithm()
        {
            _reader = new TSPFileReader();
            _parser = new SymmetricalParser();
        }

        public void Run(string fileName)
        {
            var data = _reader.Read(GlobalConsts.TSPFilePath + fileName);
            var cities = _parser.ParseData(data);
            foreach (var item in cities)
            {
                System.Console.WriteLine(item.Number + " " + item.Latitude + " " + item.Longitude);
            }
        }
    }
}