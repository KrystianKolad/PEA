using PEA.Algorithms.Interfaces;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;

namespace PEA.Algorithms.TSP
{
    public class GeneticAlgorithm : Abstract.AbstractGeneticAlgorithm,IAlgorithm
    {
        private readonly IReader _reader;
        private readonly IParser<Matrix> _parser;

        public GeneticAlgorithm()
        {
            _reader = new TSPFileReader();
            _parser = new SymmetricalParser();
        }

        public void Run(string fileName)
        {
            var data = _reader.Read(fileName);
            Matrix matrix = _parser.ParseData(data);
            System.Console.WriteLine("Rozpoczynam algorytm");
            // // TODO
            var time = this.Execute(matrix);
            System.Console.WriteLine("Algorytm został zakończony");
            System.Console.WriteLine("Wykonano w: " + time + " ms.");
        }
    }
}