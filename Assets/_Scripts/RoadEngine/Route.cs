namespace RoadEngine
{
    using System.Collections.Generic;

    public class Route
    {
        public List<string> roadConnections = new List<string>(); //Connections guid.
        public RoadGraphData specifiedRoad;
        public float length;

        public Route(List<string> inputRoadConnections, float inputLength, RoadGraphData inputSpecifiedRoad)
        {
            roadConnections = inputRoadConnections;
            length = inputLength;
            specifiedRoad = inputSpecifiedRoad;
        }
    }
}
