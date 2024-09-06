using Sirenix.OdinInspector;

namespace RoadEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AllRoads : ScriptableObject
    {
        [SerializeField] [TableList] private List<Road> roads;
        private Dictionary<string, Road> _roadsDict;

        public void Initialize()
        {
            _roadsDict = new Dictionary<string, Road>();
            
            foreach (Road road in roads)
            {
                _roadsDict.Add(road.roadName, road);
            }
        }

        public Road GetRoadSpline(string roadName)
        {
            return _roadsDict[roadName];
        }
    }
}
