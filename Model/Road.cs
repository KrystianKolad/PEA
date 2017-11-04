using PEA.Model.Interfaces;

namespace PEA.Model
{
    public class Road : IEntity
    {
        public City Start { get; set; }
        public City Destination { get; set; }

        public Road(City start, City destination)
        {
            Start = start;
            Destination = destination;
        }

    }
}