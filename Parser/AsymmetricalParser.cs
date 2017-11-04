using System;
using System.Collections.Generic;
using PEA.Model;
using PEA.Parser.Interfaces;

namespace PEA.Parser
{
    public class AsymmetricalParser : IAsymmetricalParser<Matrix>
    {
        public Matrix ParseData(string[] data)
        {
            Matrix result = new Matrix();
            try
            {
                
            }
            catch (Exception exc)
            {   
                throw exc;
            }
            return result;
        }
    }
}