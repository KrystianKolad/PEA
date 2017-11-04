using System;
using System.Collections.Generic;
using System.IO;
using PEA.DataAccess.Interfaces;

namespace PEA.DataAccess
{
    public class TSPFileReader : IReader
    {
        private const int LinesToCut = 6;
        public string[] Read(string filePath)
        {
            string[] result = null;
            try
            {
                IList<string> fileLines = new List<string>();
                using (var streamReader = new StreamReader(filePath))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null && line != "EOF")
                    {
                        fileLines.Add(line);
                    }
                }
                result = new string[fileLines.Count - LinesToCut];
                for (int i = LinesToCut; i < fileLines.Count; i++)
                {
                    result[i - LinesToCut] = fileLines[i];
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
            return result;
        }
    }
}