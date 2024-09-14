namespace PlanerEngine
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlanerManager : MonoBehaviour
    {
        [Header("Managers")] 
        [SerializeField] private OrderManager orderManager;
        [SerializeField] private PlanerUiManager uiManager;

        private List<Plan> _plans;
        private Plan _currentPlan;
        
        // Start is called before the first frame update
        public void StartPlaner()
        {
            uiManager.Initialization(this);
            orderManager.Initialization(this);

            _plans = new List<Plan>();
        }

        public void CreateNewPlan()
        {
            _currentPlan = new Plan();
            orderManager.CreateOrder();
        }

        public void AddOrder(Order order)
        {
            _currentPlan.order = order;
            uiManager.OrderAdded();

            foreach (Product product in _currentPlan.order.products)
            {
                Debug.Log(product.productName + " - " + product.amount);
            }
            Debug.Log("Total size: " + _currentPlan.order.totalSize);
        }

        public void AddRoute(RoadEngine.Route route)
        {
            _currentPlan.route = route;
            uiManager.PlanFinished();
        }
    }
}