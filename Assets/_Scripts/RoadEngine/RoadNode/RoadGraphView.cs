namespace RoadEngine
{
    
#if UNITY_EDITOR
    
    using UnityEditor.Experimental.GraphView;
    using UnityEngine.UIElements;
    using System.Collections.Generic;

    using UnityEngine;


    public class RoadGraphView : GraphView
    {

        private readonly Vector2 _basicNodeScale = new Vector2(550,200);
        public TextField roadNameTextField;
        
        public RoadGraphView()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("NodeStyle"));
            SetupZoom(ContentZoomer.DefaultMinScale,ContentZoomer.DefaultMaxScale);
            
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0,grid);
            grid.StretchToParentSize();
            
            AddElement(GenerateEntryPointNode());
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();
            ports.ForEach((port) =>
            {
                if (startPort != port && startPort.node != port.node)
                {
                    compatiblePorts.Add(port);
                }
            });         
            
            return compatiblePorts;
        }

        private Port GeneratePort(RoadGraphNode node, Direction portDirection,
            Port.Capacity capacity = Port.Capacity.Multi)
        {
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }

        private RoadGraphNode GenerateEntryPointNode()
        {
            RoadGraphNode node = new RoadGraphNode("Start");
            
            node.SetPosition(new Rect(100,200,100,150));
            node.title = node.nodeName;

            Port generatedPort = GeneratePort(node, Direction.Output);
            generatedPort.portName = "Next";
            node.outputContainer.Add(generatedPort);

            node.capabilities -= Capabilities.Movable;
            node.capabilities -= Capabilities.Deletable;
            
            node.RefreshExpandedState();
            node.RefreshPorts();
            
            return node;
        }

        public void CreateNode(string nodeName)
        {
            AddElement(CreateRoadNode(nodeName));
        }

        public RoadGraphNode CreateRoadNode(string nodeName, float distance = 0f)
        {
            RoadGraphNode node = new RoadGraphNode("Road");
            node.SetPosition(new Rect(Vector2.zero, _basicNodeScale));
            node.title = nodeName;
            node.nodeName = nodeName;
            
            var textField = new TextField();
            textField.name = "road-name-text-field";
            textField.SetValueWithoutNotify(nodeName);
            textField.RegisterValueChangedCallback(evt => node.nodeName = evt.newValue);
            node.titleContainer.Add(textField);
            

            Port outputPort = GeneratePort(node, Direction.Output);
            outputPort.portName = "Next";
            node.outputContainer.Add(outputPort);

            Button button = new Button((() => AddInputPort(node, 0f)));
            button.text = "New Input";
            node.titleContainer.Add(button);

            AddInputPort(node, distance);
            
            return node;
        }

        public void AddInputPort(RoadGraphNode node, float distance)
        {
            Port generatedPort = GeneratePort(node, Direction.Input, Port.Capacity.Single);
            int inputPortCount = node.inputContainer.Query("connector").ToList().Count;
            generatedPort.portName = $"Input {inputPortCount}";
            node.inputContainer.Add(generatedPort);

            var floatField = new FloatField();
            floatField.name = "float-field";
            floatField.RegisterValueChangedCallback(evt => generatedPort.userData = evt.newValue);
            floatField.value = distance;
            
            generatedPort.Add(floatField);
            
            node.RefreshExpandedState();
            node.RefreshPorts();
        }

        public void RoadName(string roadName)
        {
            roadNameTextField.value = roadName;
        }
        

    }
#endif
    
}
