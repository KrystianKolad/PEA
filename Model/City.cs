using PEA.Model.Interfaces;

namespace PEA.Model
{
    public class City : IEntity
    {
        public int Number { get; private set; }
        public float Longitude { get; private set; }
        public float Latitude { get; private set; }

        public City(int number, float longitude, float latitude)
        {
            Number = number;
            Longitude = longitude;
            Latitude = latitude;
        }

    }
}