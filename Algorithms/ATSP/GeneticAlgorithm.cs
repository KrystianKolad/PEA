using PEA.Algorithms.Interfaces;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;

namespace PEA.Algorithms.ATSP
{
    public class GeneticAlgorithm : Abstract.AbstractGeneticAlgorithm,IAlgorithm
    {
        private IReader _reader;
        private IParser<Matrix> _parser;

        public GeneticAlgorithm()
        {
            _reader = new TSPFileReader();
            _parser = new AsymmetricalParser();
        }

        public void Run(string fileName)
        {
            var data = _reader.Read(fileName);
            Matrix matrix = _parser.ParseData(data);
            System.Console.WriteLine("Rozpoczynam algorytm");
            // // TODO
            this.Execute(matrix);
            System.Console.WriteLine("Algorytm został zakończony");
        }
    }
}