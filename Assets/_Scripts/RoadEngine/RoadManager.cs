namespace RoadEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RoadManager : MonoBehaviour
    {
        [SerializeField] private AllRoads allRoads;
        [SerializeField] private RouteCreationManager routeManager;
        
        public void StartRoad()
        {
            allRoads.Initialize();
            routeManager.Initialize();
        }
    }
}
