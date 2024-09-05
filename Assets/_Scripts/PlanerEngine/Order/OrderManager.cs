namespace PlanerEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class OrderManager : MonoBehaviour
    {
        private int _orders;
        private List<Order> _preparedOrders;
        private List<Order> _preparingOrders;
        
        [SerializeField] private NewOrderManager newOrderManager;

        
        public void Initialization()
        {

        }

        public void CreateOrder()
        {
            
        }
        
    }
}
