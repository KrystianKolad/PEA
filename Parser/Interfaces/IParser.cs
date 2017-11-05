using PEA.Model.Interfaces;

namespace PEA.Parser.Interfaces
{
    public interface IParser<T> where T : IEntity
    {
         T ParseData(string[] data);
    }
}