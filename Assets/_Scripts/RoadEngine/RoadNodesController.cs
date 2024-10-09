namespace RoadEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    public class RoadNodesController : MonoBehaviour
    {
        public RoadGraphData roadGraphData;
        [ReadOnly]
        public List<RoadNode> nodes;

#if UNITY_EDITOR
        
        [Button]
        public void SetupRoadNodes()
        {
            Dictionary<string, RoadNode> nodesDict = new Dictionary<string, RoadNode>();
            nodes = new List<RoadNode>();
            
            foreach (Transform child in this.transform)
            {
                var node = child.GetComponent<RoadNode>();
                nodesDict[child.name] = node;
                nodes.Add(node);
            }
            
            roadGraphData.Initialize();

            foreach (RoadDataNode dataNode in roadGraphData.roadNodesDict.Values)
            {
                if (nodesDict.ContainsKey(dataNode.nodeName))
                {
                    nodesDict[dataNode.nodeName].SetNodeGui(dataNode.GUID);
                }
                else
                {
                    Debug.LogWarning("No such node representation - " + dataNode.nodeName);
                }
            }
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}
