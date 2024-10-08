namespace RoadEngine
{
    using System;
    
    [Serializable]
    public class RoadNodeConnection
    {
        public string GUID;
        public string inputNodeGUID;
        public string outputNodeGUID;
        public float distance;

        public RoadNodeConnection(string inputNode, string outputNode, float inputDistance)
        {
            GUID = Guid.NewGuid().ToString();
            inputNodeGUID = inputNode;
            outputNodeGUID = outputNode;
            distance = inputDistance;
        }
    }
}
