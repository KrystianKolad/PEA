using System;
using System.Collections.Generic;
using System.IO;
using PEA.Algorithms.Abstract;
using PEA.Algorithms.TSP;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;
using Xunit;

namespace TabuSearchTests
{
    public class TabuSearchTests
    {
        IList<string> TSPFiles;
        IList<string> ATSPFiles;
        IReader _TSPFileReader;
        IReader _ATSPFileReader;
        IParser<Matrix> _TSPParser;
        IParser<Matrix> _ATSPParser;
        AbstractTabuSearchAlgorithm _algorithm;
        Dictionary<string, long> _results;
        public TabuSearchTests()
        {
            _TSPFileReader = new TSPFileReader();
            _ATSPFileReader = new ATSPFileReader();
            _TSPParser = new SymmetricalParser();
            _ATSPParser = new AsymmetricalParser();
            _algorithm = new TabuSearchAlgorithm();
            GetFileNames();
            _results = new Dictionary<string, long>();
        }
        [Fact]
        public void CrateData()
        {
            foreach (var file in TSPFiles)
            {
                var time = _algorithm.Execute(_TSPParser.ParseData(_TSPFileReader.Read(file)));
                _results.Add(file, time);
            }
            foreach (var file in ATSPFiles)
            {
                var time = _algorithm.Execute(_ATSPParser.ParseData(_ATSPFileReader.Read(file)));
                _results.Add(file, time);
            }
            using (var streamWriter = new StreamWriter("../../../../../artifacts/tabuSearchTest.txt"))
            {
                streamWriter.WriteLine(@"Plik       Wartosc[ms]");
                foreach (var result in _results)
                {
                    var key = result.Key.Split(@"/");
                    var file = key[key.Length-1];
                    Console.WriteLine(file + ": " + result.Value.ToString());
                    streamWriter.WriteLine(file +"      " + result.Value.ToString());
                }
            }
            Assert.True(true);
        }

        private void GetFileNames()
        {
            TSPFiles = new List<string>();
            ATSPFiles = new List<string>();
            string[] TSPFileNames = System.IO.Directory.GetFiles(@"../../../../TestData/TSP/");
            for (int i = 0; i < TSPFileNames.Length; i++)
            {
                int number = i + 1;
                TSPFiles.Add(TSPFileNames[i]);
            }
            string[] ATSPFileNames = System.IO.Directory.GetFiles(@"../../../../TestData/ATSP/");
            for (int i = 0; i < ATSPFileNames.Length; i++)
            {
                int number = i + 1;
                ATSPFiles.Add(ATSPFileNames[i]);
            }
        }
    }
}
