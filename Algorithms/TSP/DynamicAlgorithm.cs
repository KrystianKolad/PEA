using System.Collections.Generic;
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
        private IParser<Matrix> _parser;

        public DynamicAlgorithm()
        {
            _reader = new TSPFileReader();
            _parser = new SymmetricalParser();
        }

        public void Run(string fileName)
        {
            string[] data = _reader.Read(fileName);
            Matrix cities = _parser.ParseData(data);
            System.Console.WriteLine("Rozpoczynam algorytm");
            // TODO
            System.Console.WriteLine("Algorytm został zakończony");
        }
    }
}