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
        Dictionary<string, double> _tspResults;
        Dictionary<string, double> _atspResults;
        public TabuSearchTests()
        {
            _TSPFileReader = new TSPFileReader();
            _ATSPFileReader = new ATSPFileReader();
            _TSPParser = new SymmetricalParser();
            _ATSPParser = new AsymmetricalParser();
            _algorithm = new TabuSearchAlgorithm();
            GetFileNames();
            _tspResults = new Dictionary<string, double>();
            _atspResults = new Dictionary<string, double>();
        }
        [Fact]
        public void CrateData()
        {
            for(int i = 1;i<4;i++)
            {
                for(int j = 1; j<4;j++)
                {
                _tspResults.Clear();
                _atspResults.Clear();
                foreach (var file in TSPFiles)
                {
                    var time = _algorithm.Execute(_TSPParser.ParseData(_TSPFileReader.Read(file)),(int)Math.Pow(10,i),5*j);
                    _tspResults.Add(file, time);
                }
                foreach (var file in ATSPFiles)
                {
                    var time = _algorithm.Execute(_ATSPParser.ParseData(_ATSPFileReader.Read(file)),(int)Math.Pow(10,i),5*j);
                    _atspResults.Add(file, time);
                }
                using (var streamWriter = new StreamWriter($"../../../../../artifacts/tabuSearchTest{Math.Pow(10,i)}_{5*j}.txt"))
                {
                    streamWriter.WriteLine(@"\begin{center}");
                    streamWriter.WriteLine(@"\begin{table}");
                    streamWriter.WriteLine($"\\caption{{Symetryczny problem dla {Math.Pow(10,i)} iteracji oraz kadencji = {5*j}}}");
                    streamWriter.WriteLine(@"\begin{table}");
                    streamWriter.WriteLine(@"\begin{tabular}{ |c|c| } ");
                    streamWriter.WriteLine(@" \hline");
                    streamWriter.WriteLine(@"Plik(wraz z ilością miast) & wynik \\ \hline");
                    foreach (var result in _tspResults)
                    {
                        var key = result.Key.Split(@"/");
                        var file = key[key.Length-1];
                        streamWriter.WriteLine(file +" & " + result.Value.ToString() + @"\\ \hline");
                    }
                    streamWriter.WriteLine(@"\end{tabular}");
                    streamWriter.WriteLine(@"\end{table}");
                    streamWriter.WriteLine(@"\end{center}");
                    streamWriter.WriteLine("");
                    streamWriter.WriteLine(@"\begin{center}");
                    streamWriter.WriteLine(@"\begin{table}");
                    streamWriter.WriteLine($"\\caption{{Asymetryczny problem dla {Math.Pow(10,i)} iteracji oraz kadencji = {5*j}}}");
                    streamWriter.WriteLine(@"\begin{table}");
                    streamWriter.WriteLine(@"\begin{tabular}{ |c|c| } ");
                    streamWriter.WriteLine(@" \hline");
                    streamWriter.WriteLine(@"Plik(wraz z ilością miast) & wynik");
                    foreach (var result in _atspResults)
                    {
                        var key = result.Key.Split(@"/");
                        var file = key[key.Length-1];
                        streamWriter.WriteLine(file +" & " + result.Value.ToString() + @"\\ \hline");
                    }
                    streamWriter.WriteLine(@"\end{tabular}");
                    streamWriter.WriteLine(@"\end{table}");
                    streamWriter.WriteLine(@"\end{center}");
                    streamWriter.WriteLine("";)
                    streamWriter.WriteLine($"\\subsubsection{{Wykres zależności czasu wykonywania od ilości miast dla symetrycznego problemu przy ilości iteracji ={Math.Pow(10,i)} oraz kadencji = {5*j} }}");
                    streamWriter.WriteLine(@"\begin{tikzpicture}[scale=1.1]
                                            \begin{axis}[
                                            xlabel={Liczba miast},
                                            ylabel={Czas[ms]},
                                            xmin=0,xmax=26,
                                            ymin=0,ymax=100,
                                            legend pos=north west,
                                            ymajorgrids=true,grid style=dotted
                                            ]

                                            \addplot[color=blue,mark=square]
                                            coordinates {")
                    foreach (var result in _tspResults)
                    {
                        var key = result.Key.Split(@"/");
                        var file = key[key.Length-1];
                        streamWriter.WriteLine("("file +"," + result.Value.ToString() + ")");
                    }
                    streamWriter.WriteLine(@"};

                                            \end{axis}
                                            \end{tikzpicture}
                                            ")
                    streamWriter.WriteLine("";)
                    streamWriter.WriteLine($"\\subsubsection{{Wykres zależności czasu wykonywania od ilości miast dla niesymetrycznego problemu przy ilości iteracji ={Math.Pow(10,i)} oraz kadencji = {5*j} }}");
                    streamWriter.WriteLine(@"\begin{tikzpicture}[scale=1.1]
                                            \begin{axis}[
                                            xlabel={Plik},
                                            ylabel={Czas[ms]},
                                            xmin=0,xmax=26,
                                            ymin=0,ymax=100,
                                            legend pos=north west,
                                            ymajorgrids=true,grid style=dotted
                                            ]

                                            \addplot[color=blue,mark=square]
                                            coordinates {")
                    foreach (var result in _atspResults)
                    {
                        var key = result.Key.Split(@"/");
                        var file = key[key.Length-1];
                        streamWriter.WriteLine("("file +"," + result.Value.ToString() + ")");
                    }
                    streamWriter.WriteLine(@"};

                                            \end{axis}
                                            \end{tikzpicture}
                                            ")

                    
                }
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
