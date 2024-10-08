using System;

namespace RoadEngine
{
    
#if UNITY_EDITOR
    
    using UnityEditor;
    using UnityEditor.UIElements;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class RoadGraph : EditorWindow
    {
        private RoadGraphView _graphView;
        private string _roadGraphName = "New Road";
        
        [MenuItem("Tools/Roads graph")]
        public static void OpenRoadGraphWindow()
        {
            var window = GetWindow<RoadGraph>();
            window.titleContent = new GUIContent("Road Graph");
        }

        private void ConstructGraphView()
        {
            _graphView = new RoadGraphView()
            {
                name = "Road Graph"
            };
            
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }

        private void GenerateToolbar()
        {
            Toolbar toolBar = new Toolbar();

            var roadGraphNameTextField = new TextField("Road Name");
            roadGraphNameTextField.SetValueWithoutNotify(_roadGraphName);
            roadGraphNameTextField.MarkDirtyRepaint();
            roadGraphNameTextField.RegisterValueChangedCallback(evt => _roadGraphName = evt.newValue);
            toolBar.Add(roadGraphNameTextField);
            
            toolBar.Add(new Button(() => RequestDataOperation(true)){text = "Save Data"});
            toolBar.Add(new Button(() => RequestDataOperation(false)){text = "Load Data"});
            
            Button nodeCreateButton = new Button(() => { _graphView.CreateNode("RoadNode"); });
            nodeCreateButton.text = "New RoadNode";
            toolBar.Add(nodeCreateButton);
            
            rootVisualElement.Add(toolBar);
        }

        private void RequestDataOperation(bool save)
        {
            if (string.IsNullOrEmpty(_roadGraphName))
            {
                EditorUtility.DisplayDialog("Invalid road name!", "Please enter a valid file name!", "Ok");
                return;
            }

            var saveUtility = RoadGraphSaveUtility.GetInstance(_graphView);
            if (save)
            {
                saveUtility.SaveGraph(_roadGraphName);
            }
            else
            {
                saveUtility.LoadGraph(_roadGraphName);
            }
        }

        private void OnEnable()
        {
            ConstructGraphView();
            GenerateToolbar();
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }
    }
#endif
    
}