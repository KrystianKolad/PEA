using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using PEA.Algorithms.Abstract;
using PEA.Algorithms.TSP;
using PEA.DataAccess;
using PEA.DataAccess.Interfaces;
using PEA.Model;
using PEA.Parser;
using PEA.Parser.Interfaces;
using Xunit;
namespace GeneticTests
{
    public class GeneticTests
    {
        IList<string> TSPFiles;
        IList<string> ATSPFiles;
        IReader _TSPFileReader;
        IReader _ATSPFileReader;
        IParser<Matrix> _TSPParser;
        IParser<Matrix> _ATSPParser;
        AbstractGeneticAlgorithm _algorithm;
        Dictionary<string, Tuple<float,double>> _tspResults;
        Dictionary<string, Tuple<float,double>> _atspResults;
        public GeneticTests()
        {
            _TSPFileReader = new TSPFileReader();
            _ATSPFileReader = new ATSPFileReader();
            _TSPParser = new SymmetricalParser();
            _ATSPParser = new AsymmetricalParser();
            _algorithm = new GeneticAlgorithm();
            GetFileNames();
            _tspResults = new Dictionary<string, Tuple<float,double>>();
            _atspResults = new Dictionary<string, Tuple<float,double>>();
        }
        [Fact]
        public void CrateData()
        {
            int length = 4;
            for (int i = 1; i < length; i++)
            {
                for (int j = 1; j < length; j++)
                {
                    for (int k = 1; k < 2; k++)
                    {
                        for (int l = 1; l < 2; l++)
                        {
                            _tspResults.Clear();
                            _atspResults.Clear();
                            foreach (var file in TSPFiles)
                            {
                                var time = _algorithm.Execute(_TSPParser.ParseData(_TSPFileReader.Read(file)),(int)Math.Pow(10,i),20*j,10*k,5*l);
                                _tspResults.Add(file, time);
                            }
                            foreach (var file in ATSPFiles)
                            {
                                var time = _algorithm.Execute(_ATSPParser.ParseData(_ATSPFileReader.Read(file)),(int)Math.Pow(10,i),20*j,10*k,5*l);
                                _atspResults.Add(file, time);
                            }
                            using (var streamWriter = new StreamWriter($"../../../../../artifacts/geneticTest{Math.Pow(10,i)}_{20*j}_{10*k}_{5*l}.txt"))
                            {
                                _tspResults = (from result in _tspResults orderby result.Value.Item2 ascending select result).ToDictionary(g => g.Key, v => v.Value);
                                _atspResults = (from result in _atspResults orderby result.Value.Item2 ascending select result).ToDictionary(g => g.Key, v => v.Value);
                                streamWriter.WriteLine(@"\begin{center}");
                                streamWriter.WriteLine(@"\begin{table}[htbp] \raggedright");
                                streamWriter.WriteLine($"\\caption{{Symetryczny problem dla {Math.Pow(10,i)} iteracji, populacji o wielkosci {20*j},{10*k} krzyzowaniach i {5*l} mutacjach }}");
                                streamWriter.WriteLine(@"\begin{tabular}{ |c|c|c| } ");
                                streamWriter.WriteLine(@" \hline");
                                streamWriter.WriteLine(@"Plik(wraz z ilością miast) & Czas[ms] & Wynik \\ \hline");
                                foreach (var result in _tspResults)
                                {
                                    var key = result.Key.Split(@"/");
                                    var file = key[key.Length-1];
                                    streamWriter.WriteLine(file + " & " + result.Value.Item2.ToString() + " & " + result.Value.Item1.ToString() + @"\\ \hline");
                                }
                                streamWriter.WriteLine(@"\end{tabular}");
                                streamWriter.WriteLine(@"\end{table}");
                                streamWriter.WriteLine(@"\end{center}");
                                streamWriter.WriteLine("");
                                streamWriter.WriteLine(@"\begin{center}");
                                streamWriter.WriteLine(@"\begin{table}[htbp] \raggedright");
                                streamWriter.WriteLine($"\\caption{{Asymetryczny problem dla {Math.Pow(10,i)} iteracji, populacji o wielkosci {20*j},{10*k} krzyzowaniach i {5*l} mutacjach }}");
                                streamWriter.WriteLine(@"\begin{tabular}{ |c|c|c| } ");
                                streamWriter.WriteLine(@" \hline");
                                streamWriter.WriteLine(@"Plik(wraz z ilością miast) & Czas[ms] & Wynik \\ \hline");
                                foreach (var result in _atspResults)
                                {
                                    var key = result.Key.Split(@"/");
                                    var file = key[key.Length-1];
                                    streamWriter.WriteLine(file +" & " + result.Value.Item2.ToString() + " & " + result.Value.Item1.ToString() + @"\\ \hline");
                                }
                                streamWriter.WriteLine(@"\end{tabular}");
                                streamWriter.WriteLine(@"\end{table}");
                                streamWriter.WriteLine(@"\end{center}");
                                streamWriter.WriteLine("");
                                streamWriter.WriteLine($"\\subsubsection{{Wykres zależności czasu wykonywania od ilości miast dla symetrycznego problemu przy {Math.Pow(10,i)} iteracji, populacji o wielkosci {20*j},{10*k} krzyzowaniach i {5*l} mutacjach }}");
                                streamWriter.WriteLine(@"\begin{tikzpicture}[scale=1.1]
\begin{axis}[
xlabel={Plik},
ylabel={Czas[ms]},
xmin=0,xmax=105,
ymin=0,ymax=100,
legend pos=north west,
ymajorgrids=true,grid style=dotted
]

\addplot[color=blue,mark=square]
coordinates {");
                                IList<Tuple<int,double>> items = new List<Tuple<int,double>>();
                                foreach (var result in _tspResults)
                                {
                                    var key = result.Key.Split(@"/");
                                    var file = key[key.Length-1].Replace(".atsp",string.Empty);
                                    items.Add(new Tuple<int,double>(Int32.Parse(Regex.Match(file, @"\d+").Value ),result.Value.Item2));
                                }
                                items = items.OrderBy(x=>x.Item1).ToList();
                                foreach(var item in items)
                                {
                                    streamWriter.WriteLine("(" + item.Item1 +"," + item.Item2 + ")");
                                }
                                streamWriter.WriteLine(@"};

\end{axis}
\end{tikzpicture}
");
                                streamWriter.WriteLine("");
                                streamWriter.WriteLine($"\\subsubsection{{Wykres zależności czasu wykonywania od ilości miast dla niesymetrycznego problemu przy {Math.Pow(10,i)} iteracji, populacji o wielkosci {20*j},{10*k} krzyzowaniach i {5*l} mutacjach }}");
                                streamWriter.WriteLine(@"\begin{tikzpicture}[scale=1.1]
\begin{axis}[
xlabel={Plik},
ylabel={Czas[ms]},
xmin=0,xmax=130,
ymin=0,ymax=100,
legend pos=north west,
ymajorgrids=true,grid style=dotted
]

\addplot[color=blue,mark=square]
coordinates {");
                                items = new List<Tuple<int,double>>();
                                foreach (var result in _atspResults)
                                {
                                    var key = result.Key.Split(@"/");
                                    var file = key[key.Length-1].Replace(".atsp",string.Empty);
                                    items.Add(new Tuple<int,double>(Int32.Parse(Regex.Match(file, @"\d+").Value ),result.Value.Item2));
                                }
                                items = items.OrderBy(x=>x.Item1).ToList();
                                foreach(var item in items)
                                {
                                    streamWriter.WriteLine("(" + item.Item1 +"," + item.Item2 + ")");
                                }
                                streamWriter.WriteLine(@"};

\end{axis}
\end{tikzpicture}
");
                            }
                        }
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
