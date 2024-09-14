namespace RoadEngine
{
    using System.Collections.Generic;

    public class Route
    {
        public List<Road> roads = new List<Road>();
        public float length;

        public Route(List<Road> inputRoads, float inputLength)
        {
            roads = inputRoads;
            length = inputLength;
        }
    }
}
