using System;
using System.Collections.Generic;
using PEA.Consts;
using PEA.Model;
using PEA.Parser.Interfaces;

namespace PEA.Parser
{
    public class SymmetricalParser : ISymmetricalParser<City>
    {
        public IList<City> ParseData(string[] data)
        {
            IList<City> result = new List<City>();
            int numberOfCities = data.Length;
            try
            {
                for (int i = GlobalConsts.TSPLinesToCutOff; i < numberOfCities; i++)
                {
                    string[] line = new string[3];
                    line = data[i].Split(" ");
                    result.Add(
                        new City(
                            Int32.Parse(line[0]),
                            float.Parse(line[1]),
                            float.Parse(line[2])
                            )
                        );
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