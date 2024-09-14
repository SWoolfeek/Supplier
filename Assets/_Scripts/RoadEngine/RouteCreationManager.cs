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
        private List<Road> roads;
        [SerializeField] [ReadOnly]
        private float totalLength;
        
        [SerializeField] private AllRoads allRoads;
        [SerializeField] private List<RoadNode> nodes;
        
        [Header("Managers")] 
        [SerializeField] private PlanerEngine.PlanerManager planerManager;
        
        [Header("Ui elements")] 
        [SerializeField] private GameObject applyButton;
        
        [HideInInspector]
        public UnityEvent onDisableNodes = new UnityEvent();

        private Dictionary<string, RoadNode> _nodesDict = new Dictionary<string, RoadNode>();
        private string _startingNode;
        private bool _routeCreation;
        private bool _routeFinished;
        
        public void Initialize()
        {
            foreach (RoadNode node in nodes)
            {
                _nodesDict.Add(node.nodeName, node);
                node.Initialize(this);
                if (node.nodeType == RoadNode.RoadNodeType.Start)
                {
                    _startingNode = node.nodeName;
                }
            }
        }

        public void FinishRoute()
        {
            applyButton.SetActive(false);
            planerManager.AddRoute(new Route(roads, totalLength));
        }

        [Button]
        public void StartRouteCreation()
        {
            Debug.Log("StartRouteCreation");
            _routeFinished = false;
            _routeCreation = true;
            route = new List<string>();
            // Road. Need to be extended when system will be finished.
            roads = new List<Road>();
            route.Add(_startingNode);
            ActivateNodes(_nodesDict[_startingNode].nextNodes);
        }

        public void AddNode(string nodeName)
        {
            if (_routeCreation)
            {
                route.Add(nodeName);
                if (_nodesDict[nodeName].nodeType != RoadNode.RoadNodeType.End)
                {
                    ActivateNodes(_nodesDict[nodeName].nextNodes);
                }
                else
                {
                    onDisableNodes.Invoke();
                    RouteFinished(true);
                    Debug.Log("Route is finished.");
                }

                int routeLasElementIndex = route.Count - 1;

                Road road = allRoads.GetRoadSpline(route[routeLasElementIndex - 1] + route[routeLasElementIndex]);
                totalLength += road.length;
                
                roads.Add(road);
            }
        }

        private void RouteFinished(bool state)
        {
            _routeFinished = true;
            applyButton.SetActive(true);
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
                        foreach (Road road in roads)
                        {
                            totalLength += road.length;
                        }
                        
                        route.RemoveRange(nodeIndex, route.Count - nodeIndex);
                        
                        
                    }

                    if (_routeFinished)
                    {
                        RouteFinished(false);
                    }

                    ActivateNodes(_nodesDict[route.Last()].nextNodes);
                }
            }
        }
        
        private void ActivateNodes(List<RoadNode> nodesToActivate)
        {
            Debug.Log("ActivateNodes");
            onDisableNodes.Invoke();
            
            foreach (RoadNode node in nodesToActivate)
            {
                node.ActivateNode();
            }
        }
    }
}
