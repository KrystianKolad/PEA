using System;
using System.Collections.Generic;
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
            try
            {
                _reader = new TSPFileReader();
                //SymmetricalGeneticTSP();
                AsymmetricalGeneticTSP();
            }
            catch (Exception exc)
            {
                
                System.Console.WriteLine(exc.Message);
            }
        }

        private static void SymmetricalGeneticTSP()
        {
            _parser = new SymmetricalParser();
            _algorithm = new GeneticAlgorithm();
            var data = _reader.Read(@"Data/berlin52.tsp");
            IList<City> cities = _parser.ParseData(data);
        }

        private static void AsymmetricalGeneticTSP()
        {
            _parser = new AsymmetricalParser();
            _algorithm = new GeneticAlgorithm();
            var data = _reader.Read(@"Data/ftv55.atsp");
        }
    }
}
