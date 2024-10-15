namespace PlanerEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    
    public class PlanController : MonoBehaviour
    {
        [Header("Ui Elements")] 
        [SerializeField] private TMP_Text lengthText;
        [SerializeField] private TMP_Text sizeText;
        [SerializeField] private TMP_Text daysText;
        [SerializeField] private TMP_Text couriersText;
        [SerializeField] private TMP_Text idText;

        private PlanerManager _manager;
        private string _planId;
        
        public void Initialization(Plan plan, PlanerManager manager)
        {
            lengthText.text = plan.route.length.ToString();
            sizeText.text = plan.order.totalSize.ToString();
            daysText.text = plan.daysLeft.ToString();
            couriersText.text = plan.couriersAmount.ToString();

            _planId = plan.planId;
            //idText.text = plan.planId;
            name = _planId;

            _manager = manager;
        }

        public void DeleteRecord()
        {
            Debug.Log("Destroy it!");
            _manager.RemovePlan(_planId);
            Destroy(this.gameObject);
        }
        
    }
}
