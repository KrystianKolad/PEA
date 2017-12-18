using System;
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
            _reader = new ATSPFileReader();
            _parser = new AsymmetricalParser();
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
            var time = this.Execute(matrix,iterations, cadency);
            System.Console.WriteLine("Algorytm został zakończony");
            System.Console.WriteLine("Wykonano w: " + time + " ms.");
        }
    }
}