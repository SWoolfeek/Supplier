namespace RoadEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RoadManager : MonoBehaviour
    {
        [SerializeField] private AllRoads allRoads;
        [SerializeField] private List<RoadGraphData> roadGraphsData;
        [SerializeField] private RouteCreationManager routeManager;

        [SerializeField] private RoadNodesController mainRoadNodesController; //Can be extended to List<RoadNodesController>
        
        public void StartRoad()
        {
            foreach (RoadGraphData roadGraph in roadGraphsData)
            {
                roadGraph.Initialize();
            }
            
            allRoads.Initialize();
            routeManager.Initialize(mainRoadNodesController);
            
            Debug.Log("Road Manager - Initialized");
        }
    }
}
