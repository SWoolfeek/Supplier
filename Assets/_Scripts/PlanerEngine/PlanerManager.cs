namespace PlanerEngine
{
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlanerManager : MonoBehaviour
    {
        [Header("Managers")] 
        [SerializeField] private OrderManager orderManager;
        
        // Start is called before the first frame update
        public void StartPlaner()
        {
            orderManager.Initialization();
        }
    }
}