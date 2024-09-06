namespace RoadEngine
{
    using UnityEngine.Splines;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using Sirenix.OdinInspector;

    [Serializable]
    public class Road
    {
        [ReadOnly]
        public string roadName;

        [SerializeField] [OnValueChanged("CreateRodeName")]
        private string startNode;
        [SerializeField] [OnValueChanged("CreateRodeName")]
        private string endNode;
        
        [MinValue(0.1f)] public float length = .1f;

        private void CreateRodeName()
        {
            roadName = startNode + endNode;
        }
    }
}
