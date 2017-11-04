using PEA.Algorithms.Interfaces;

namespace PEA.Factory.Interfaces
{
    public interface IFactory
    {
        IAlgorithm GetAlgorithm(int number);
    }
}