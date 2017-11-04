using System;
using PEA.Algorithms;
using PEA.Algorithms.Interfaces;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Model.Interfaces;
using PEA.Parser;
using PEA.Parser.Interfaces;

namespace PEA
{
    class Program
    {
        private static IReader _reader;
        private static IParser<City> _parser;
        private static IAlgorithm _algorithm;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static void SymmetricalGeneticTSP()
        {
            _reader = new SymmetricalReader();
            _parser = new SymmetricalParser();
            _algorithm = new GeneticAlgorithm();
        }

        private static void AsymmetricalGeneticTSP()
        {
            _reader = new AsymmetricalReader();
            _parser = new AsymmetricalParser();
            _algorithm = new GeneticAlgorithm();
        }
    }
}
