using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PEA.Consts;
using PEA.Model;
using PEA.Parser.Interfaces;

namespace PEA.Parser
{
    public class AsymmetricalParser : IAsymmetricalParser<Matrix>
    {
        public Matrix ParseData(string[] data)
        {
            Matrix result = null;
            int rows = data.Length - GlobalConsts.ATSPLinesToCutOff;
            int columns = 0;
            Console.WriteLine("Parsuję dane.");
            try
            {
                IList<string[]> temporaryRows = new List<string[]>();
                for (int i = GlobalConsts.ATSPLinesToCutOff; i < data.Length; i++)
                {
                    int rowLenght = 0;
                    string[] oldRow = data[i].Split(" ");
                    for (int j = 0; j < oldRow.Length; j++)
                    {
                        if (!oldRow[j].Equals(string.Empty))
                        {
                            rowLenght++;
                        }
                    }
                    if (rowLenght > columns)
                    {
                        columns = rowLenght;
                    }
                }
                for (int i = GlobalConsts.ATSPLinesToCutOff; i < data.Length; i++)
                {
                    string[] newRow = new string[columns];
                    int rowLenght = 0;
                    string[] oldRow = data[i].Split(" ");
                    for (int j = 0; j < oldRow.Length; j++)
                    {
                        if (!oldRow[j].Equals(string.Empty))
                        {
                            newRow[rowLenght] = oldRow[j];
                            rowLenght++;
                        }
                    }
                    temporaryRows.Add(newRow);
                }
                result = new Matrix(rows,columns);
                int row=0;
                foreach (var item in temporaryRows)
                {
                    for (int i = 0; i < columns; i++)
                    {
                        if(item[i]!=null)
                        {
                            result.SetField(row,i,float.Parse(item[i]));
                        }
                        else
                        {
                            result.SetField(row,i,null);
                        }
                    }
                    row++;
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