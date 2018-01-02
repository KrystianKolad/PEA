using System;
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
            _reader = new ATSPFileReader();
            _parser = new AsymmetricalParser();
        }

        public void Run(string fileName)
        {
            var data = _reader.Read(fileName);
            Matrix matrix = _parser.ParseData(data);
            System.Console.WriteLine("Rozpoczynam algorytm");
            // // TODO
            Console.WriteLine("Podaj ilość pokoleń(iteracji)");
            int iterations = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Podaj ilość elementów populacji");
            int membersCount = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Podaj ilość krzyzowań");
            int crossCount = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Podaj ilośc mutacji");
            int mutationCount = Int32.Parse(Console.ReadLine());
            var time = this.Execute(matrix,iterations,membersCount,crossCount,mutationCount).Item2;
            System.Console.WriteLine("Algorytm został zakończony");
            System.Console.WriteLine("Wykonano w: " + time + " ms.");
        }
    }
}