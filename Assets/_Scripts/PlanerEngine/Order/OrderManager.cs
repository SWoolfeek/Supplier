namespace PlanerEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class OrderManager : MonoBehaviour
    {
        private PlanerManager _manager;
        
        [SerializeField] private NewOrderManager newOrderManager;
        [SerializeField] private GameObject newOrderWindow;
        

        
        public void Initialization(PlanerManager manager)
        {
            _manager = manager;
            newOrderManager.Initialization(this);
        }

        public void CreateOrder()
        {
            newOrderWindow.SetActive(true);
            newOrderManager.UpdateAllProductsInOrder();
        }

        public void OrderCompleted(Order order)
        {
            newOrderWindow.SetActive(false);
            _manager.AddOrder(order);
        }
        
    }
}
