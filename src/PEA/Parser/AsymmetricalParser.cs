using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PEA.Consts;
using PEA.Model;
using PEA.Parser.Interfaces;

namespace PEA.Parser
{
    public class AsymmetricalParser : IParser<Matrix>
    {
        public Matrix ParseData(string[] data)
        {
            Matrix result = null;
            int columns = int.Parse(data[0]);
            int rows = (data.Length -1)/columns;
            int currentPlace=1;
            Console.WriteLine("Parsuję dane.");
            try
            {
                result=new Matrix(rows,columns);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        result.SetField(i,j,float.Parse(data[currentPlace]));
                        currentPlace++;
                    }
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