namespace PlanerEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;


    public class PlanerUiManager : MonoBehaviour
    {
        private PlanerManager _manager;

        [Header("Ui elements")] 
        [SerializeField] private Button newPlanButton;
        
        // Start is called before the first frame update
        public void Initialization(PlanerManager manager)
        {
            _manager = manager;
        }

        public void CreateNewPlan()
        {
            _manager.CreateNewPlan();
            newPlanButton.interactable = false;
        }

        public void OrderAdded()
        {
            
        }

        public void PlanFinished()
        {
            
        }
    }
}
