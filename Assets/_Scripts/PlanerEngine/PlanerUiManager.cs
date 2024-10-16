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
        [SerializeField] private GameObject map;
        [SerializeField] private GameObject roads;
        [SerializeField] private GameObject plansRepresentation;
        
        // Start is called before the first frame update
        public void Initialization(PlanerManager manager)
        {
            _manager = manager;
        }

        public void CreateNewPlan()
        {
            _manager.CreateNewPlan();
            newPlanButton.interactable = false;
            plansRepresentation.SetActive(false);
        }

        public void OrderAdded()
        {
            map.SetActive(true);
            roads.SetActive(true);
        }

        public void PlanFinished()
        {
            map.SetActive(false);
            roads.SetActive(false);
            plansRepresentation.SetActive(true);
            Debug.Log("Plan is finished.");
            newPlanButton.interactable = true;
        }
    }
}
