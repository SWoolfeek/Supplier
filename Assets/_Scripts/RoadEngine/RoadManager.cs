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
        
        public void StartRoad()
        {
            foreach (RoadGraphData roadGraph in roadGraphsData)
            {
                roadGraph.Initialize();
            }
            
            allRoads.Initialize();
            routeManager.Initialize();
        }
    }
}
