using UnityEngine;

namespace RoadEngine
{
    using System.Collections.Generic;
    using System;
    
    [Serializable]
    public class RoadDataNode
    {
        public string GUID;
        public string nodeName;
        public Rect position;
        public List<string> inputConnections;
        public List<string> outputConnections;

        public RoadDataNode(string inputGuid, string inputName, Rect inputPosition)
        {
            GUID = inputGuid;
            nodeName = inputName;
            position = inputPosition;
            inputConnections = new List<string>();
            outputConnections = new List<string>();
        }
    }
}
