namespace PlanerEngine
{
    using CoreSystems;
    using RoadEngine;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;
    
    public class PlanerManager : MonoBehaviour
    {
        [Header("Managers")] 
        [SerializeField] private OrderManager orderManager;
        [SerializeField] private PlanerUiManager uiManager;
        [SerializeField] private RouteCreationManager routeCreationManager;
        [SerializeField] private CouriersPlanerManager couriersPlanerManager;
        
        [Header("Plans in Ui")]
        [SerializeField] private Transform planParent;
        [SerializeField] private GameObject planUiObject;

        public GameParameters gameParameters;

        [ReadOnly][SerializeField] private List<Plan> _plans;
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

        public void AddCouriers(int amount, int daysLeft)
        {
            _currentPlan.couriersAmount = amount;
            _currentPlan.daysLeft = daysLeft;
            uiManager.PlanFinished();
            
            _plans.Add(_currentPlan);
            CreatePlanUiRepresentation(_currentPlan);
        }

        private void CreatePlanUiRepresentation(Plan plan)
        {
            var createdPlan = Instantiate(planUiObject,planParent);
            createdPlan.GetComponent<PlanController>().Initialization(plan,this);
        }

        public void RemovePlan(string planId)
        {
            foreach (Plan plan in _plans)
            {
                if (plan.planId == planId)
                {
                    _plans.Remove(plan);
                    break;
                }
            }
        }
    }
}