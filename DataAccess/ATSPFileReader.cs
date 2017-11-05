using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PEA.Consts;
using PEA.DataAccess.Interfaces;

namespace PEA.DataAccess
{
    public class ATSPFileReader : IReader
    {
        public string[] Read(string filePath)
        {
            string[] result = null;
            string dimension = String.Empty;
            IList<string> file;
            Console.WriteLine("Wczytuję dane.");
            try
            {
                IList<string> fileLines = new List<string>();
                using (var streamReader = new StreamReader(filePath))
                {
                    string line = null;
                    while (!(line = streamReader.ReadLine()).Contains(DataReaderConsts.EDGE_WEIGHT_SECTION))
                    {
                        if (line.Contains(DataReaderConsts.DIMENSION_KEYWORD))
                        {
                            string[] splitLine = line.Split(" ");
                            dimension = splitLine[splitLine.Length - 1];
                        }
                    }
                    file = streamReader.ReadToEnd().Split().Where(x => x.All(char.IsDigit)).ToList();
                }
                fileLines.Add(dimension);
                for (int i = 0; i < file.Count; i++)
                {
                    try
                    {
                        int.Parse(file[i]);
                        fileLines.Add(file[i]);
                    }
                    catch (Exception)
                    {
                    }
                }
                result = new string[fileLines.Count];
                for (int i = 0; i < fileLines.Count; i++)
                {
                    result[i] = fileLines[i];
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
            Console.WriteLine("Dane wczytane.");
            return result;
        }
    }
}