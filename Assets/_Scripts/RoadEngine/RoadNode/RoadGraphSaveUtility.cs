namespace RoadEngine
{
#if UNITY_EDITOR
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;
    using UnityEditor.Experimental.GraphView;
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine.UIElements;

    public class RoadGraphSaveUtility
    {
        private RoadGraphView _targetGraphView;
        private RoadGraphData _cachedGraph;
        
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

                GenerateNodeData(inputNode.GUID, inputNode.nodeName, nodeConnection.GUID, inputNode.GetPosition(),false);
                GenerateNodeData(outputNode.GUID, outputNode.nodeName, nodeConnection.GUID, outputNode.GetPosition(),true);
            }
            
            roadContainer.SetData(roadName, _dataNodes.Values.ToList(), _roadNodeConnections);
            
            AssetDatabase.CreateAsset(roadContainer, $"Assets/Data/Road Engine/{roadName} RoadGraph.asset");
            AssetDatabase.SaveAssets();
        }
        
        public void LoadGraph(object roadGraph)
        {
            _cachedGraph = roadGraph as RoadGraphData;
            _targetGraphView.RoadName(_cachedGraph.roadName);
            
            if (_cachedGraph==null)
            {
                EditorUtility.DisplayDialog("File not found!", "Target graph does not exist!", "Ok");
                return;
            }

            ClearGraph();
            CreateNodes();
        }

        private void CreateNodes()
        {
            Dictionary<string, RoadGraphNode> graphNodes = new Dictionary<string, RoadGraphNode>();
            List<string> wasAsOutput = new List<string>();
            
            var startingNode = Nodes.Find(x => x.nodeName == "Start");
            Debug.Log(startingNode.GUID);
            graphNodes[startingNode.GUID] = startingNode;
            
            _cachedGraph.Initialize();

            foreach (var connection in _cachedGraph.GetAllConnections())
            {
                RoadGraphNode inputNode;
                if (graphNodes.ContainsKey(connection.inputNodeGUID))
                {
                    inputNode = graphNodes[connection.inputNodeGUID];
                }
                else
                {
                    inputNode = CreateNode(connection.inputNodeGUID);
                    graphNodes[connection.inputNodeGUID] = inputNode;
                }
                
                RoadGraphNode outputNode;
                if (graphNodes.ContainsKey(connection.outputNodeGUID))
                {
                    outputNode = graphNodes[connection.outputNodeGUID];
                    if (wasAsOutput.Contains(connection.outputNodeGUID))
                    {
                        _targetGraphView.AddInputPort(outputNode, connection.distance);
                    }
                }
                else
                {
                    outputNode = CreateNode(connection.outputNodeGUID, false, connection.distance);
                    graphNodes[connection.outputNodeGUID] = outputNode;
                    wasAsOutput.Add(connection.outputNodeGUID);
                }

                if (!wasAsOutput.Contains(connection.outputNodeGUID))
                {
                    outputNode.inputContainer[0].Q<FloatField>().value = connection.distance;
                    outputNode.inputContainer[0].Q<Port>().userData = connection.distance;
                    wasAsOutput.Add(connection.outputNodeGUID);
                    
                }
                
                LinkNodes(inputNode.outputContainer[0].Q<Port>(), outputNode.inputContainer[outputNode.inputContainer.childCount - 1].Q<Port>());
            }
        }

        private void LinkNodes(Port input, Port output)
        {
            var edge = new Edge
            {
                output = input,
                input = output
            };
            
            edge?.input.Connect(edge);
            edge?.output.Connect(edge);
            _targetGraphView.Add(edge);
        }

        private RoadGraphNode CreateNode(string guid, bool input = true, float distance = 0f)
        {
            RoadGraphNode node;
            
            Debug.Log("Created with GUID - " + guid);
            
            if (input)
            {
                node = _targetGraphView.CreateRoadNode(_cachedGraph.roadNodesDict[guid].nodeName);
            }
            else
            {
                node = _targetGraphView.CreateRoadNode(_cachedGraph.roadNodesDict[guid].nodeName, distance);
                node.inputContainer[0].Q<Port>().userData = distance;
            }
            
            node.GUID = guid;
            node.titleContainer.Q<TextField>().value = _cachedGraph.roadNodesDict[guid].nodeName;
            
            node.SetPosition(_cachedGraph.roadNodesDict[guid].position);
            _targetGraphView.AddElement(node);
            
            return node;
        }

        private void ClearGraph()
        {
            Nodes.Find(x => x.nodeName == "Start").GUID = _cachedGraph.GetAllNodes().Find(x => x.nodeName == "Start").GUID;
            Debug.Log(_cachedGraph.GetAllNodes()[0].GUID);

            foreach (var node in Nodes)
            {
                if (node.nodeName == "Start")
                {
                    continue;
                }
                
                Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));
                _targetGraphView.RemoveElement(node);
            }
        }

        private void GenerateNodeData(string nodeGuid, string nodeName, string nodeConnection, Rect nodePosition, bool input)
        {
            if (!_dataNodes.ContainsKey(nodeGuid))
            {
                _dataNodes.Add(nodeGuid, new RoadDataNode(nodeGuid, nodeName, nodePosition));
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
