namespace RoadEngine
{
    using System.Collections.Generic;
    using System;
    
    [Serializable]
    public class RoadDataNode
    {
        public string GUID;
        public string nodeName;
        public List<string> inputConnections;
        public List<string> outputConnections;

        public RoadDataNode(string inputGuid, string inputName)
        {
            GUID = inputGuid;
            nodeName = inputName;
            inputConnections = new List<string>();
            outputConnections = new List<string>();
        }
    }
}
