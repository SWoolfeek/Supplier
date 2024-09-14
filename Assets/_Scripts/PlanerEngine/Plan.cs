namespace PlanerEngine
{
    using System;

    
    [Serializable]
    public class Plan
    {
        public int planId;
        public Order order;
        public RoadEngine.Route route;
    }
}
