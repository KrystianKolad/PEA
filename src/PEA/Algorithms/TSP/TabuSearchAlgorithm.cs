using System;
using PEA.Algorithms.Interfaces;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;

namespace PEA.Algorithms.TSP
{
    public class TabuSearchAlgorithm : Abstract.AbstractTabuSearchAlgorithm,IAlgorithm
    {
        private readonly IReader _reader;
        private readonly IParser<Matrix> _parser;

        public TabuSearchAlgorithm()
        {
            _reader = new TSPFileReader();
            _parser = new SymmetricalParser();
        }

        public void Run(string fileName)
        {
            var data = _reader.Read(fileName);
            Matrix matrix = _parser.ParseData(data);
            System.Console.WriteLine("Podaj ilosc iteracji");
            int iterations = Int32.Parse(Console.ReadLine());
            System.Console.WriteLine("Podaj wielkosc kadencji");
            int cadency = Int32.Parse(Console.ReadLine());
            System.Console.WriteLine("Rozpoczynam algorytm");
            // // TODO
            var time = this.Execute(matrix,iterations, cadency).Item2;
            System.Console.WriteLine("Algorytm został zakończony");
            System.Console.WriteLine("Wykonano w: " + time + " ms.");
        }
    }
}