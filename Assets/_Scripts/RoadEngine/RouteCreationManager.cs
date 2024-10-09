namespace RoadEngine
{
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;
    using UnityEngine.Events;
    using System.Linq;
    
    public class RouteCreationManager : MonoBehaviour
    {
        [SerializeField] [ReadOnly]
        private List<string> route;
        // Road. Need to be extended when system will be finished.
        [SerializeField] [ReadOnly]
        private List<string> roads;
        [SerializeField] [ReadOnly]
        private float totalLength;
        
        [Header("Managers")] 
        [SerializeField] private PlanerEngine.PlanerManager planerManager;
        
        [Header("Ui elements")] 
        [SerializeField] private GameObject applyButton;
        
        [HideInInspector]
        public UnityEvent onDisableNodes = new UnityEvent();

        private Dictionary<string, RoadNode> _nodesDict = new Dictionary<string, RoadNode>();
        private Dictionary<string, string> _connectionsDict = new Dictionary<string, string>();
        private string _startingNode;
        private bool _routeCreation;
        private bool _routeFinished;
        private RoadNodesController _currentNodesController;
        
        public void Initialize(RoadNodesController nodesController)
        {
            _currentNodesController = nodesController;
            _currentNodesController.roadGraphData.Initialize();
            foreach (RoadNode node in _currentNodesController.nodes)
            {
                _nodesDict.Add(node.nodeName, node);
                node.Initialize(this);
                if (node.nodeType == RoadNode.RoadNodeType.Start)
                {
                    _startingNode = node.nodeName;
                    Debug.Log("Starting node name - " + _startingNode);
                }
            }
        }

        public void FinishRoute()
        {
            applyButton.SetActive(false);
            planerManager.AddRoute(new Route(roads, totalLength, _currentNodesController.roadGraphData));
        }

        [Button]
        public void StartRouteCreation()
        {
            Debug.Log("StartRouteCreation");
            _routeFinished = false;
            _routeCreation = true;
            route = new List<string>();
            // Road. Need to be extended when system will be finished.
            roads = new List<string>();
            route.Add(_startingNode);
            _nodesDict[_startingNode].StateStartingNode(true);
            _connectionsDict.Clear();
            ActivateNodes(GetRoadNodesAbleToConnect(_startingNode));
        }

        public void AddNode(string nodeName)
        {
            if (_routeCreation)
            {
                route.Add(nodeName);
                totalLength += _currentNodesController.roadGraphData.roadNodeConnectionsDict[_connectionsDict[nodeName]]
                    .distance;
                roads.Add(_connectionsDict[nodeName]);
                if (_nodesDict[nodeName].nodeType == RoadNode.RoadNodeType.End)
                {
                    RouteFinished(true);
                    Debug.Log("Route is finished.");
                }
                else
                {
                    if (_routeFinished)
                    {
                        RouteFinished(false);
                    }
                }
                
                ActivateNodes(GetRoadNodesAbleToConnect(nodeName));
            }
        }

        private void RouteFinished(bool state)
        {
            _routeFinished = state;
            applyButton.SetActive(state);
        }

        public void RemoveNode(string nodeName)
        {
            if (_routeCreation)
            {
                if (route.Contains(nodeName))
                {
                    int nodeIndex = route.IndexOf(nodeName);

                    if (nodeIndex == 0)
                    {
                        foreach (string node in route)
                        {
                            _nodesDict[node].DeActivateToChoseNode();
                        }
                        route.Clear();
                        route.Add(_startingNode);
                        // Road. Need to be extended when system will be finished.
                        roads.Clear();
                        totalLength = 0f;
                    }
                    else
                    {
                        // Road. Need to be extended when system will be finished.
                        roads.RemoveRange(roads.Count - (route.Count - nodeIndex), route.Count - nodeIndex);

                        totalLength = 0;
                        foreach (string road in roads)
                        {
                            totalLength += _currentNodesController.roadGraphData.roadNodeConnectionsDict[road].distance;
                        }

                        for (int i = nodeIndex; i < route.Count; i++)
                        {
                            _nodesDict[route[i]].DeActivateToChoseNode();
                        }
                        
                        route.RemoveRange(nodeIndex, route.Count - nodeIndex);
                        
                        
                    }
                    
                    onDisableNodes.Invoke();

                    if (_routeFinished)
                    {
                        RouteFinished(false);
                    }

                    ActivateNodes(GetRoadNodesAbleToConnect(route.Last()));
                }
            }
        }

        //This works only for OUTPUT nodes. From player to enemies
        private List<RoadNode> GetRoadNodesAbleToConnect(string nodeName)
        {
            _connectionsDict.Clear();
            List<RoadNode> result = new List<RoadNode>();
            
            foreach (string connection in _currentNodesController.roadGraphData.roadNodesDict[_nodesDict[nodeName].nodeGui].outputConnections)
            {
                RoadNode node = _nodesDict[
                    _currentNodesController.roadGraphData
                        .roadNodesDict[
                            _currentNodesController.roadGraphData.roadNodeConnectionsDict[connection].outputNodeGUID]
                        .nodeName];
                if (!route.Contains(node.nodeName))
                {
                    _connectionsDict[node.nodeName] = connection;
                    result.Add(node);
                }
            }
            
            foreach (string connection in _currentNodesController.roadGraphData.roadNodesDict[_nodesDict[nodeName].nodeGui].inputConnections)
            {
                RoadNode node = _nodesDict[
                    _currentNodesController.roadGraphData
                        .roadNodesDict[
                            _currentNodesController.roadGraphData.roadNodeConnectionsDict[connection].inputNodeGUID]
                        .nodeName];
                if (!route.Contains(node.nodeName))
                {
                    _connectionsDict[node.nodeName] = connection;
                    result.Add(node);
                }
            }

            if (result.Count < 1 && !_routeFinished)
            {
               Debug.LogWarning("Ooops, it is loop road!"); 
            }
            
            return result;
        }
        
        private void ActivateNodes(List<RoadNode> nodesToActivate)
        {
            onDisableNodes.Invoke();
            
            foreach (RoadNode node in nodesToActivate)
            {
                if (node.IsOwned())
                {
                    node.ActivateToChoseNode();
                }
            }
        }
    }
}
