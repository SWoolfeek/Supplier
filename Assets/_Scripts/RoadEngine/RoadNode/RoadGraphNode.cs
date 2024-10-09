namespace RoadEngine
{
    
#if UNITY_EDITOR
    
    using UnityEditor.Experimental.GraphView;
    using System;

    public class RoadGraphNode : Node
    {
        public string GUID;
        public string nodeName;

        public RoadGraphNode(string inputNodeName)
        {
            GUID = Guid.NewGuid().ToString();
            this.nodeName = inputNodeName;
        }
    }
    
#endif
    
}
