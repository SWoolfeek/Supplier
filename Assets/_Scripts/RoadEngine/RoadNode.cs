namespace RoadEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using System;
    using UnityEngine;
    using Sirenix.OdinInspector;
    
#if UNITY_EDITOR
    using UnityEditor;
#endif


    public class RoadNode : MonoBehaviour
    {
        [OnValueChanged("ChangeName")]
        public string nodeName;
        [ReadOnly]
        public string nodeGui;
        public RoadNodeType nodeType = RoadNodeType.Intermediate;
        [SerializeField] private RoadNodeState _nodeState;
        

        [Header("Colors")] 
        [SerializeField] private SpriteRenderer _sprite;
        public Color basicColor = Color.white;
        public Color activeColor = Color.green;
        public Color activeToChoseColor = Color.green;
        public Color enemyColor = Color.green;
       
        private RouteCreationManager _manager;
        [ReadOnly][SerializeField] private bool _isActiveToChose;
        private bool _isActive;
        
        
        
        public enum RoadNodeType
        {
            Start = 0,
            Intermediate = 1,
            End = 2
        }
        
        public enum RoadNodeState
        {
            Owned = 0,
            Active = 1,
            ActiveToChose = 2,
            EnemiesOwn = 3
        }

        public void Initialize(RouteCreationManager inputManager)
        {
            _manager = inputManager;
            _manager.onDisableNodes.AddListener(DeactivateToChoseNode);
            ChangeNodeColour();
        }

        public bool IsOwned()
        {
            if (_nodeState == RoadNodeState.Owned)
            {
                return true;
            }

            return false;
        }

        public void ActivateToChoseNode()
        {
            _isActiveToChose = true;
            _nodeState = RoadNodeState.ActiveToChose;
            ChangeNodeColour();
        }
        
        public void DeActivateToChoseNode()
        {
            if (_isActive)
            {
                _isActive = false;
                _nodeState = RoadNodeState.Owned;
                ChangeNodeColour();
            }
            
        }

        public void StateStartingNode(bool state)
        {
            if (nodeType == RoadNodeType.Start)
            {
                _isActive = state;
                if (_isActive)
                {
                    _nodeState = RoadNodeState.Active;
                    ChangeNodeColour();
                }
                else
                {
                    _nodeState = RoadNodeState.Owned;
                    ChangeNodeColour();
                }
            }
        }

        private void OnMouseDown()
        {
            if (_isActiveToChose)
            {
                _manager.AddNode(nodeName);
                _isActive = true;
                _isActiveToChose = false;
                _nodeState = RoadNodeState.Active;
                ChangeNodeColour();
            }
        }
        
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1) & (_isActive || nodeType == RoadNodeType.Start)) {
                _manager.RemoveNode(nodeName);
            }
        }

        private void DeactivateToChoseNode()
        {
            if (_isActiveToChose)
            {
                _isActiveToChose = false;
                _isActive = false;
                _nodeState = RoadNodeState.Owned;
                ChangeNodeColour();
            }
        }

        private void ChangeNodeColour()
        {
            switch (_nodeState)
            {
                case RoadNodeState.Owned:
                    _sprite.color = basicColor;
                    break;
                case RoadNodeState.Active:
                    _sprite.color = activeColor;
                    break;
                case RoadNodeState.ActiveToChose:
                    _sprite.color = activeToChoseColor;
                    break;
                case RoadNodeState.EnemiesOwn:
                    _sprite.color = enemyColor;
                    break;
            }
        }
        
#if UNITY_EDITOR
        private void ChangeName()
        {
            name = nodeName;
        }

        public void SetNodeGui(string gui)
        {
            nodeGui = gui;
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif

        private void OnDestroy()
        {
            _manager.onDisableNodes.RemoveListener(DeactivateToChoseNode);
        }
    }
}
