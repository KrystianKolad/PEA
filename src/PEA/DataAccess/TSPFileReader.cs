using System;
using System.Collections.Generic;
using System.IO;
using PEA.Consts;
using PEA.DataAccess.Interfaces;

namespace PEA.DataAccess
{
    public class TSPFileReader : IReader
    {
        public string[] Read(string filePath)
        {
            string[] result = null;
            Console.WriteLine("Wczytuję dane.");
            try
            {
                IList<string> fileLines = new List<string>();
                using (var streamReader = new StreamReader(filePath))
                {
                    string line = null;
                    while (!(line = streamReader.ReadLine()).Contains(DataReaderConsts.NODE_COORD_SECTION))
                    {
                    }
                    while ((line = streamReader.ReadLine()) != null && !line.Contains(DataReaderConsts.END_OF_FILE))
                    {
                        fileLines.Add(line);
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