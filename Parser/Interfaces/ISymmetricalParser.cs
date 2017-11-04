using System.Collections.Generic;
using PEA.Model.Interfaces;

namespace PEA.Parser.Interfaces
{
    public interface ISymmetricalParser<T> where T : IEntity
    {
        IList<T> ParseData(string[] data);
    }
}