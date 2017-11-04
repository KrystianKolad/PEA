using System;
using System.Collections.Generic;
using PEA.Algorithms.Interfaces;
using PEA.Consts;
using PEA.Factory;
using PEA.Factory.Interfaces;

namespace PEA
{
    class Program
    {
        private static readonly IFactory _factory;
        private static IAlgorithm _algorithm;
        private static Dictionary<int, string> TSPFiles;
        private static Dictionary<int, string> ATSPFiles;
        static Program()
        {
            _factory = new AlgorithmFactory();
            TSPFiles = new Dictionary<int, string>();
            TSPFiles.Add(1, "berlin52.tsp");
            TSPFiles.Add(2, "ch130.tsp");
            TSPFiles.Add(3, "d18512.tsp");
            ATSPFiles = new Dictionary<int, string>();
            ATSPFiles.Add(1, "ftv55.atsp");
            ATSPFiles.Add(2, "ft70.atsp");
            ATSPFiles.Add(3, "rbg403.atsp");
        }
        static void Main(string[] args)
        {
            int algorithmNumber;
            int fileNumber;
            string fileName;
            string @continue;
            bool working = true;
            try
            {
                while (working)
                {
                System.Console.WriteLine(GlobalConsts.AlgorithmChoice);
                algorithmNumber = Int32.Parse(Console.ReadLine());
                _algorithm = _factory.GetAlgorithm(algorithmNumber);
                System.Console.WriteLine(
                    algorithmNumber % 2 != 0 ? GlobalConsts.TSPFileChoice : GlobalConsts.ATSPFileChoice
                    );
                fileNumber = Int32.Parse(Console.ReadLine());
                fileName = algorithmNumber % 2 != 0 ? TSPFiles.GetValueOrDefault(fileNumber) : ATSPFiles.GetValueOrDefault(fileNumber);
                _algorithm.Run(fileName);
                System.Console.WriteLine(GlobalConsts.AfterMessage);
                @continue = Console.ReadLine();
                working =@continue.Equals("y") || @continue.Equals("Y");
                }
            }
            catch (Exception exc)
            {

                System.Console.WriteLine(exc.Message);
            }
        }
    }
}
