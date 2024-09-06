using System;

namespace RoadEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;


    public class RoadNode : MonoBehaviour
    {
        [OnValueChanged("ChangeName")]
        public string nodeName;
        public RoadNodeType nodeType = RoadNodeType.Intermediate;
        public List<RoadNode> nextNodes;
        

        [Header("Colors")] 
        [SerializeField] private SpriteRenderer _sprite;
        public Color basicColor = Color.white;
        public Color activeColor = Color.green;
       
        private RouteManager _manager;
        private bool _isActive;
        
        
        public enum RoadNodeType
        {
            Start = 0,
            Intermediate = 1,
            End = 2
        }

        public void Initialize(RouteManager inputManager)
        {
            _manager = inputManager;
            _manager.onDisableNodes.AddListener(DeactivateNode);
        }

        public void ActivateNode()
        {
            _isActive = true;
            _sprite.color = activeColor;
        }

        private void OnMouseDown()
        {
            if (_isActive)
            {
                _manager.AddNode(nodeName);
            }
        }
        
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1)) {
                _manager.RemoveNode(nodeName);
            }
        }

        private void DeactivateNode()
        {
            if (_isActive)
            {
                _isActive = false;
                _sprite.color = basicColor;
            }
        }
        
#if UNITY_EDITOR
        private void ChangeName()
        {
            name = nodeName;
        }
#endif

        private void OnDisable()
        {
            _manager.onDisableNodes.RemoveListener(DeactivateNode);
        }
    }
}
