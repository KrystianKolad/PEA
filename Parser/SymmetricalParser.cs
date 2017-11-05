using System;
using System.Collections.Generic;
using PEA.Consts;
using PEA.Model;
using PEA.Parser.Interfaces;

namespace PEA.Parser
{
    public class SymmetricalParser : IParser<Matrix>
    {
        public Matrix ParseData(string[] data)
        {
            Matrix result = null;
            IList<City> cities = new List<City>();
            int numberOfCities = data.Length;
            Console.WriteLine("Parsuję dane.");
            try
            {
                for (int i = 0; i < numberOfCities; i++)
                {
                    string[] line = new string[3];
                    line = data[i].Split(" ");
                    cities.Add(
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
            Console.WriteLine("Dane sparsowane.");
            return result;
        }
    }
}