namespace RoadEngine
{

    using UnityEngine;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;

    public class RoadGraphData : ScriptableObject
    {
        [SerializeField] private string roadName;
        
        [SerializeField] [ReadOnly]
        private List<RoadDataNode> _roadDataNodes;
        [SerializeField] [ReadOnly]
        private List<RoadNodeConnection> _roadNodeConnections;

        private Dictionary<string, RoadDataNode> _roadNodesDict;
        private Dictionary<string, RoadNodeConnection> _roadNodeConnectionsDict;

        public void Initialize()
        {
            foreach (RoadDataNode node in _roadDataNodes)
            {
                _roadNodesDict.Add(node.GUID, node);
            }
            
            foreach (RoadNodeConnection connection in _roadNodeConnections)
            {
                _roadNodeConnectionsDict.Add(connection.GUID, connection);
            }
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
