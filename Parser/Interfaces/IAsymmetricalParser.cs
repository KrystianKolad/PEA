using PEA.Model.Interfaces;

namespace PEA.Parser.Interfaces
{
    public interface IAsymmetricalParser<T> where T : IEntity
    {
        T ParseData(string[] data);
    }
}