namespace RoadEngine
{

    using UnityEngine;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    public class RoadGraphData : ScriptableObject
    {
        public string roadName;
        
        [SerializeField] [ReadOnly]
        private List<RoadDataNode> _roadDataNodes;
        [SerializeField] [ReadOnly]
        private List<RoadNodeConnection> _roadNodeConnections;

        public Dictionary<string, RoadDataNode> roadNodesDict;
        public Dictionary<string, RoadNodeConnection> roadNodeConnectionsDict;

        public void Initialize()
        {
            roadNodesDict = new Dictionary<string, RoadDataNode>();
            roadNodeConnectionsDict = new Dictionary<string, RoadNodeConnection>();
            
            foreach (RoadDataNode node in _roadDataNodes)
            {
                roadNodesDict.Add(node.GUID, node);
            }
            
            foreach (RoadNodeConnection connection in _roadNodeConnections)
            {
                roadNodeConnectionsDict.Add(connection.GUID, connection);
            }
        }

        public List<RoadDataNode> GetAllNodes()
        {
            return _roadDataNodes;
        }
        
        public List<RoadNodeConnection> GetAllConnections()
        {
            return _roadNodeConnections;
        }

        public void SetData( string inputRoadName, List<RoadDataNode> roadDataNodes, List<RoadNodeConnection> roadNodeConnections)
        {
            roadName = inputRoadName;
            name = roadName + " RoadGraph";
            _roadDataNodes = roadDataNodes;
            _roadNodeConnections = roadNodeConnections;
        }
    }
}
