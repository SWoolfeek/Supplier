namespace RoadEngine
{
#if UNITY_EDITOR
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor;

    public class RoadGraphSaveUtility
    {
        private RoadGraphView _targetGraphView;
        
        private Dictionary<string, RoadDataNode> _dataNodes = new Dictionary<string, RoadDataNode>();
        private List<RoadNodeConnection> _roadNodeConnections = new List<RoadNodeConnection>();

        private List<Edge> Edges => _targetGraphView.edges.ToList();
        private List<RoadGraphNode> Nodes => _targetGraphView.nodes.ToList().Cast<RoadGraphNode>().ToList();

        public static RoadGraphSaveUtility GetInstance(RoadGraphView targetGraphView)
        {
            return new RoadGraphSaveUtility
            {
                _targetGraphView = targetGraphView
            };
        }

        public void SaveGraph(string roadName)
        {
            _dataNodes.Clear();
            _roadNodeConnections.Clear();
            
            if (!Edges.Any()) return;

            var roadContainer = ScriptableObject.CreateInstance<RoadGraphData>();

            foreach (Edge edge in Edges)
            {
                var inputNode = edge.output.node as RoadGraphNode;
                var outputNode = edge.input.node as RoadGraphNode;


                float distance = 0;
                if (edge.input.userData is float floatValue)
                {
                    distance = floatValue;
                }
                
                var nodeConnection = new RoadNodeConnection(inputNode.GUID,outputNode.GUID,distance);
                _roadNodeConnections.Add(nodeConnection);

                GenerateNodeData(inputNode.GUID, inputNode.nodeName, nodeConnection.GUID, false);
                GenerateNodeData(outputNode.GUID, outputNode.nodeName, nodeConnection.GUID, true);
            }
            
            roadContainer.SetData(roadName, _dataNodes.Values.ToList(), _roadNodeConnections);
            
            AssetDatabase.CreateAsset(roadContainer, $"Assets/Data/Road Engine/{roadName} RoadGraph.asset");
            AssetDatabase.SaveAssets();
        }
        
        public void LoadGraph(string roadName)
        {
            
        }

        private void GenerateNodeData(string nodeGuid, string nodeName, string nodeConnection, bool input)
        {
            if (!_dataNodes.ContainsKey(nodeGuid))
            {
                _dataNodes.Add(nodeGuid, new RoadDataNode(nodeGuid, nodeName));
            }

            if (input)
            {
                _dataNodes[nodeGuid].inputConnections.Add(nodeConnection);
            }
            else
            {
                _dataNodes[nodeGuid].outputConnections.Add(nodeConnection);
            }
            
        }
    }
#endif
}
