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
        private static string TSPFileChoice = "Wybierz plik:";
        private static string ATSPFileChoice;
        static Program()
        {
            _factory = new AlgorithmFactory();
            GetFileNames();
        }

        private static void GetFileNames()
        {
            TSPFiles = new Dictionary<int, string>();
            ATSPFiles = new Dictionary<int, string>();
            string[] TSPFileNames = System.IO.Directory.GetFiles(GlobalConsts.TSPFilePath);
            for (int i = 0; i < TSPFileNames.Length; i++)
            {
                int number = i + 1;
                TSPFiles.Add(number, TSPFileNames[i]);
                TSPFileChoice += Environment.NewLine + number + ". " + TSPFileNames[i];
            }
            string[] ATSPFileNames = System.IO.Directory.GetFiles(GlobalConsts.ATSPFilePath);
            for (int i = 0; i < ATSPFileNames.Length; i++)
            {
                int number = i + 1;
                ATSPFiles.Add(number, ATSPFileNames[i]);
                ATSPFileChoice += Environment.NewLine + number + ". " + ATSPFileNames[i];
            }
        }

        static void Main(string[] args)
        {
            int algorithmNumber;
            int fileNumber;
            string fileName;
            string @continue;
            bool working = true;
            while (working)
            {
                try
                {
                    System.Console.WriteLine(GlobalConsts.AlgorithmChoice);
                    algorithmNumber = Int32.Parse(Console.ReadLine());
                    if (algorithmNumber == 0)
                    {
                        working = false;
                        break;
                    }
                    _algorithm = _factory.GetAlgorithm(algorithmNumber);
                    System.Console.WriteLine(
                        algorithmNumber % 2 != 0 ? TSPFileChoice : ATSPFileChoice
                        );
                    fileNumber = Int32.Parse(Console.ReadLine());
                    fileName = algorithmNumber % 2 != 0 ? TSPFiles.GetValueOrDefault(fileNumber) : ATSPFiles.GetValueOrDefault(fileNumber);
                    _algorithm.Run(fileName);
                    System.Console.WriteLine(GlobalConsts.AfterMessage);
                    @continue = Console.ReadLine();
                    working = @continue.Equals("y") || @continue.Equals("Y");
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("Ta funkcjonalnosc nie została jeszcze zaimplementowana");
                }
                catch (Exception exc)
                {
                    System.Console.WriteLine(exc.Message);
                }
            }
        }
    }
}
