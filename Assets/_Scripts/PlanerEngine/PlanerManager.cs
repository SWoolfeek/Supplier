using CoreSystems;

namespace PlanerEngine
{
    using RoadEngine;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlanerManager : MonoBehaviour
    {
        [Header("Managers")] 
        [SerializeField] private OrderManager orderManager;
        [SerializeField] private PlanerUiManager uiManager;
        [SerializeField] private RouteCreationManager routeCreationManager;
        [SerializeField] private CouriersPlanerManager couriersPlanerManager;

        public GameParameters gameParameters;

        private List<Plan> _plans;
        private Plan _currentPlan;
        
        // Start is called before the first frame update
        public void StartPlaner()
        {
            uiManager.Initialization(this);
            orderManager.Initialization(this);
            couriersPlanerManager.Initialization(this);

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
            
            routeCreationManager.StartRouteCreation();
        }

        public void AddRoute(Route route)
        {
            _currentPlan.route = route;
            couriersPlanerManager.ModifyCouriers(_currentPlan.order.totalSize,_currentPlan.route.length);
        }

        public void AddCouriers(int amount)
        {
            _currentPlan.couriersAmount = amount;
            uiManager.PlanFinished();
        }
    }
}