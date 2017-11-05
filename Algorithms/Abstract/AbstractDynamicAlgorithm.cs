using PEA.Model;

namespace PEA.Algorithms.Abstract
{
    public abstract class AbstractDynamicAlgorithm
    {
        public void Execute(Matrix matrix)
        {
            matrix.Introduce();
        }
    }
}