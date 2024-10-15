namespace PlanerEngine
{
    using System;

    
    [Serializable]
    public class Plan
    {
        public string planId;
        public Order order;
        public RoadEngine.Route route;
        public int couriersAmount;
        public int daysLeft;

        public Plan()
        {
            planId = Guid.NewGuid().ToString();
        }
    }
}
