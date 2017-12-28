using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                    line = data[i].Split();
                    line = line.Where(x => CanParse(x)).ToArray();
                    cities.Add(
                        new City(
                            Int32.Parse(line[0]),
                            float.Parse(line[1], CultureInfo.InvariantCulture),
                            float.Parse(line[2], CultureInfo.InvariantCulture)
                            )
                        );
                }
                result = this.CreateMatrix(cities);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            Console.WriteLine("Dane sparsowane.");
            return result;
        }

        private Matrix CreateMatrix(IList<City> cities)
        {
            int rows = cities.Count;
            int columns = cities.Count;
            Matrix matrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    float data = this.GetDistance(cities[i], cities[j]);
                    matrix.SetField(i, j, data);
                }
            }
            return matrix;
        }

        private float GetDistance(City from, City to)
        {
            float result = from.GetDistance(to);
            return result == 0 ? GlobalConsts.MaximumMatrixValue : result;
        }

        private bool CanParse(string toParse)
        {
            try
            {
                float.Parse(toParse, CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
    }
}